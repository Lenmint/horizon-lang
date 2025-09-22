using System.Text.RegularExpressions;

namespace HorizonCompiler.Tokenize;

/// <summary>
/// Tokenize file content
/// </summary>
public class Lexer
{
    private string _content = "";
    private int _position;
    private int _line = 1;
    private int _column;

    /// Reserved keywords
    private static readonly Dictionary<string, TokenKind> _keywords = new()
    {
        { "const", TokenKind.Const },
        { "var", TokenKind.Var },

        { "void", TokenKind.Void },
        { "null", TokenKind.Null },
        { "true", TokenKind.Boolean },
        { "false", TokenKind.Boolean },

        { "and", TokenKind.JointOperator },
        { "or", TokenKind.JointOperator },
        { "not", TokenKind.NotOperator },
    };

    /// <summary>
    /// Start tokenize file content
    /// </summary>
    /// <param name="content">File content</param>
    /// <returns></returns>
    public List<Token> Tokenize(string content)
    {
        // Set defaults values
        Defaults();

        // Get char list
        _content = content;

        // Create list of tokens
        var tokens = new List<Token>();

        while (!IsEmpty())
        {
            if (IsSkippable()) // Skip trash characters like whitespaces and \n
            {
                // Track a new line
                if (Move() == '\n')
                {
                    _line++;
                    _column = 0;
                }

                continue;
            }

            var token = ProduceTokens();
            if (token.kind is not TokenKind.EmptyToken)
                tokens.Add(token);
        }

        // Create [EndOfFile] token
        tokens.Add(MakeEOF());

        // Return produced tokens
        return tokens;
    }

    #region Make Tokens

    private Token ProduceTokens()
    {
        return Current() switch
        {
            '@' => MakeToken(TokenKind.AtSign, Move().ToString()),
            '$' => MakeToken(TokenKind.DollarSign, Move().ToString()),
            '?' => MakeToken(TokenKind.QuestionMark, Move().ToString()),
            ',' => MakeToken(TokenKind.Comma, Move().ToString()),
            ';' => MakeToken(TokenKind.Semicolon, Move().ToString()),
            '(' => MakeToken(TokenKind.OpenParen, Move().ToString()),
            ')' => MakeToken(TokenKind.CloseParen, Move().ToString()),
            '{' => MakeToken(TokenKind.OpenBrace, Move().ToString()),
            '}' => MakeToken(TokenKind.CloseBrace, Move().ToString()),
            '[' => MakeToken(TokenKind.OpenBracket, Move().ToString()),
            ']' => MakeToken(TokenKind.CloseBracket, Move().ToString()),

            // More than one character token like numbers, strings and identifiers, etc...
            _ => MakeTokens(),
        };
    }

    /// <summary>
    /// Create a token record
    /// </summary>
    /// <param name="kind">Kind of the new token</param>
    /// <param name="value">Token value</param>
    /// <param name="offset">Token location column offset</param>
    /// <returns>New token record</returns>
    private Token MakeToken(TokenKind kind, string value, int offset = 0)
    {
        return new Token(
            kind,
            value,
            new Location(_line, _column - (value.Length + offset)),
            new Location(_line, _column));
    }

    /// <summary>
    /// Sorted tokens collection
    /// </summary>
    private Token MakeTokens()
    {
        // Integer, Float, Double or Long token
        if (IsInteger())
            return MakeNumberToken(false);

        // Identifier or Reserved token
        if (IsAlphabet())
            return MakeIdentifierToken();

        // Dot token or Floating number token
        if (Current() == '.')
        {
            Move();
            if (!IsEmpty() && IsInteger())
                // Float or Double token
                return MakeNumberToken(true);

            // Dot token
            return MakeToken(TokenKind.Dot, ".");
        }

        // String token
        if (Current() == '\"')
        {
            return MakeStringToken();
        }

        // Char Token
        if (Current() == '\'')
        {
            return MakeCharToken();
        }

        // Operator tokens
        return MakeOperatorToken();
    }

    private Token MakeIdentifierToken()
    {
        var startColumn = _column;
        var identifier = "";
        var first_is_alpha = false;

        while (!IsEmpty())
        {
            if (IsAlphabet() || Current() is '_')
            {
                first_is_alpha = true;
                identifier += Move();
            }
            else if (IsInteger() && first_is_alpha)
            {
                identifier += Move();
            }
            else
            {
                break;
            }
        }

        // Check if key is written only from '_' character.
        if (identifier.Trim(['_']) == string.Empty)
        {
            throw new LexerException($"Invalid identifier name: '{identifier}'.", _line, startColumn);
        }

        // Handle recursive keywords and identifiers.
        return _keywords.TryGetValue(identifier, out var kind)
            ? MakeToken(kind, identifier)
            : MakeToken(TokenKind.Identifier, identifier);
    }

    private Token MakeNumberToken(bool dotStarted)
    {
        var startColumn = _column - (dotStarted ? 1 : 0); // 1 for start dot 
        var number = dotStarted ? "0." : "";

        var isFloat = dotStarted;

        while (!IsEmpty())
        {
            if (IsInteger())
            {
                number += Move();
                continue;
            }

            if (!isFloat && Current() == '.')
            {
                isFloat = true;
                number += Move();
                continue;
            }

            break;
        }

        var num_type = "";
        if (!IsEmpty())
        {
            if (IsAlphabet())
                num_type = Current().ToString();
        }

        if (num_type == "")
            return MakeToken(isFloat ? TokenKind.Float : TokenKind.Integer, number);

        var lower = num_type.ToLower()[0];
        switch (lower)
        {
            case 'd':
                Move();
                return MakeToken(TokenKind.Double, number);

            case 'f':
                Move();
                return MakeToken(TokenKind.Float, number);

            case 'l':
            {
                Move();
                return MakeToken(TokenKind.Long, number);
            }
            case 'i':
            {
                Move();
                return MakeToken(TokenKind.Integer, number);
            }

            case 'b':
                Move();

                if (int.Parse(number) is >= 0 and <= 255) return MakeToken(TokenKind.Byte, number);

                throw new LexerException($"Invalid byte value: '{number}'.", _line, startColumn);

            default:
            {
                return MakeToken(TokenKind.Integer, number);
            }
        }
    }

    private Token MakeStringToken()
    {
        var startColumn = _column;
        Move();
        var value = "";
        var escapesLength = 0;
        var closed = false;

        while (!IsEmpty())
        {
            if (Current() == '\\')
            {
                var escape = Move().ToString(); // lest escape will apply

                if (Current() is 'u')
                {
                    escape += Move();
                    if (_content.Length >= 4)
                    {
                        for (var i = 0; i < 4; i++)
                        {
                            if (!IsAlphabet() && !IsInteger())
                            {
                                throw new LexerException("Invalid Unicode escape sequence.", _line, _column);
                            }

                            escape += Move();
                        }
                    }
                    else
                    {
                        throw new LexerException("Incomplete Unicode escape sequence.", _line, _column);
                    }
                }
                else if (Current() is 'x')
                {
                    escape += Move();
                    if (_content.Length >= 2)
                    {
                        for (var i = 0; i < 2; i++)
                        {
                            if (!IsAlphabet() && !IsInteger())
                            {
                                throw new LexerException("Invalid Hexadecimal escape sequence.", _line, _column);
                            }

                            escape += Move();
                        }
                    }
                    else
                    {
                        throw new LexerException("Incomplete Hexadecimal escape sequence.", _line, _column);
                    }
                }
                else escape += Move();

                escapesLength += escape.Length - 1;
                value += Regex.Unescape(escape);
                continue;
            }

            if (Current() == '"')
            {
                Move();
                closed = true;
                break;
            }

            value += Move();
        }

        if (closed)
            return MakeToken(TokenKind.String, value,
                2 + escapesLength); // Offset: String quotes count + escapes length if exist

        throw new LexerException("Missing string closing quote.", _line, startColumn);
    }

    private Token MakeCharToken()
    {
        var startColumn = _column;
        string value;
        var escape = string.Empty;

        Move(); // Skip first quote.

        if (Current() == '\\')
        {
            escape = Move().ToString();

            if (Current() is 'u')
            {
                escape += Move();
                if (_content.Length >= 4)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        if (!IsAlphabet() && !IsInteger())
                        {
                            throw new LexerException("Invalid Unicode escape sequence.", _line, _column);
                        }

                        escape += Move();
                    }
                }
                else
                {
                    throw new LexerException("Incomplete Unicode escape sequence.", _line, _column);
                }
            }
            else if (Current() is 'x')
            {
                escape += Move();
                if (_content.Length >= 2)
                {
                    for (var i = 0; i < 2; i++)
                    {
                        if (!IsAlphabet() && !IsInteger())
                        {
                            throw new LexerException("Invalid Hexadecimal escape sequence.", _line, _column);
                        }

                        escape += Move();
                    }
                }
                else
                {
                    throw new LexerException("Incomplete Hexadecimal escape sequence.", _line, _column);
                }
            }
            else
                escape += Move();

            value = Regex.Unescape(escape);
        }
        else
        {
            if (Current() == '\'')
            {
                throw new LexerException("Missing char value", _line, startColumn);
            }

            if (Current() != ' ' && IsSkippable())
            {
                throw new LexerException("Invalid char character.", _line, _column);
            }

            value = Move().ToString();
        }

        if (value.Length is > 1 or 0)
        {
            throw new LexerException("Char expect one character only.", _line, startColumn);
        }

        if (Current() != '\'')
        {
            throw new LexerException("Missing char closing quote.", _line, startColumn);
        }

        Move();
        return MakeToken(TokenKind.Char, value,
            2 + (escape.Length > 0 ? escape.Length - 1 : 0)); // Offset: Char quotes count + escape length if exist
    }

    private Token MakeOperatorToken()
    {
        var startColumn = _column;
        switch (Current())
        {
            case '=':
            {
                Move();
                if (!IsEmpty())
                {
                    if (Current() == '=')
                    {
                        Move();
                        return MakeToken(TokenKind.CompareOperator, "==");
                    }

                    if (Current() == '>')
                    {
                        Move();
                        return MakeToken(TokenKind.CompareOperator, ">=");
                    }
                }

                return MakeToken(TokenKind.EqualsOperator, "=");
            }

            case '!':
            {
                Move();
                if (!IsEmpty() && Current() == '=')
                {
                    Move();
                    return MakeToken(TokenKind.CompareOperator, "!=");
                }

                return MakeToken(TokenKind.NotOperator, "!");
            }

            case '<':
            {
                Move();
                if (!IsEmpty() && Current() == '=')
                {
                    Move();
                    return MakeToken(TokenKind.CompareOperator, "<=");
                }

                return MakeToken(TokenKind.CompareOperator, "<");
            }

            case '>':
            {
                Move();
                if (!IsEmpty() && Current() == '=')
                {
                    Move();
                    return MakeToken(TokenKind.CompareOperator, ">=");
                }

                return MakeToken(TokenKind.CompareOperator, ">");
            }

            case '|':
            {
                Move();
                if (!IsEmpty() && Current() == '|')
                {
                    Move();
                    return MakeToken(TokenKind.JointOperator, "||");
                }

                throw new LexerException("Invalid operator: \"|\"", _line, _column);
            }

            case '&':
            {
                Move();
                if (!IsEmpty() && Current() == '&')
                {
                    Move();
                    return MakeToken(TokenKind.JointOperator, "&&");
                }

                return MakeToken(TokenKind.Ampersand, "&");
            }

            case ':':
            {
                Move();
                if (!IsEmpty() && Current() == ':')
                {
                    Move();
                    return MakeToken(TokenKind.DoubleColon, "::");
                }

                return MakeToken(TokenKind.Colon, ":");
            }

            case '+':
            {
                Move();
                if (!IsEmpty())
                {
                    if (Current() == '+')
                    {
                        Move();
                        return MakeToken(TokenKind.AssignmentOperator, "++");
                    }

                    if (Current() == '=')
                    {
                        Move();
                        return MakeToken(TokenKind.AssignmentOperator, "+=");
                    }
                }

                return MakeToken(TokenKind.BinaryOperator, "+");
            }

            case '-':
            {
                Move();
                if (!IsEmpty())
                {
                    if (Current() == '-')
                    {
                        Move();
                        return MakeToken(TokenKind.AssignmentOperator, "++");
                    }

                    if (Current() == '=')
                    {
                        Move();
                        return MakeToken(TokenKind.AssignmentOperator, "-=");
                    }
                }

                return MakeToken(TokenKind.BinaryOperator, "-");
            }

            case '*':
            {
                Move();
                if (!IsEmpty() && Current() == '=')
                {
                    Move();
                    return MakeToken(TokenKind.AssignmentOperator, "*=");
                }

                return MakeToken(TokenKind.BinaryOperator, "*");
            }

            case '/':
            {
                Move();
                if (!IsEmpty() && Current() == '=')
                {
                    Move();
                    return MakeToken(TokenKind.AssignmentOperator, "/=");
                }

                if (!IsEmpty() && Current() == '/')
                {
                    // One line Comment detected
                    Move();
                    RemoveOneLineComment();
                    return MakeToken(TokenKind.EmptyToken, "");
                }

                if (!IsEmpty() && Current() == '*')
                {
                    // Multi lines Comment detected
                    Move();
                    RemoveMultiLineComment();
                    return MakeToken(TokenKind.EmptyToken, "");
                }

                return MakeToken(TokenKind.BinaryOperator, "/");
            }

            case '%':
            {
                Move();
                if (!IsEmpty() && Current() == '=')
                {
                    Move();
                    return MakeToken(TokenKind.AssignmentOperator, "%=");
                }

                return MakeToken(TokenKind.AssignmentOperator, "%");
            }

            default:
            {
                throw new LexerException($"Unimplemented or Invalid token: '{Current()}'", _line, _column);
            }
        }
    }

    private Token MakeEOF()
    {
        return MakeToken(TokenKind.EndOfFile, string.Empty);
    }

    #endregion

    #region Tools

    /// <summary>
    /// Set default location tacking values
    /// </summary>
    private void Defaults()
    {
        _line = 1;
        _column = 0;
    }

    /// <summary>
    /// Check if the file content is end
    /// </summary>
    /// <returns></returns>
    private bool IsEmpty() => _position >= _content.Length;

    /// <summary>
    /// Get current active character
    /// </summary>
    private char Current() => _content[_position];

    /// <summary>
    /// Move one character
    /// </summary>
    /// <returns></returns>
    private char Move()
    {
        _column++;
        var item = Current();
        _position++;
        return item;
    }

    /// <summary>
    /// Check if current character is \n, \r, \t or whitespace
    /// </summary>
    private bool IsSkippable() => Current() is '\n' or '\r' or '\t' or ' ';

    /// <summary>
    /// Check if current character is an integer number between 0 and 9
    /// </summary>
    private bool IsInteger() => Current() is >= '0' and <= '9';

    /// <summary>
    /// Check if current character is an alphabet character from 'a => z' or 'A => Z'
    /// </summary>
    private bool IsAlphabet() => Current() is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

    #endregion

    private void RemoveOneLineComment()
    {
        while (!IsEmpty())
        {
            if (Current() == '\n')
            {
                // End of comment
                break;
            }

            Move();
        }
    }

    private void RemoveMultiLineComment()
    {
        var startColumn = _column - 2; // 2 for start comment signs '/*'
        var isEnd = false;
        while (!IsEmpty())
        {
            var c = Current();
            if (Current() == '*')
            {
                Move();

                if (Current() == '/')
                {
                    // End of comment
                    Move();
                    isEnd = true;
                    break;
                }
            }

            Move();
        }

        if (isEnd) return;
        throw new LexerException("Missing end for the multi-line comment.", _line, startColumn);
    }
}