using HorizonCompiler.Parse.Core;
using HorizonCompiler.Parse.Expressions;
using HorizonCompiler.Parse.Statements;
using HorizonCompiler.Tokenize;

namespace HorizonCompiler.Parse;

public class Parser
{
    private List<Token> tokens = [];

    public Tree ProduceTree(List<Token> _tokens)
    {
        tokens = _tokens;

        List<Statement> body = [];

        while (!IsEmpty())
        {
            var statement = Current().kind switch
            {
                _ => ParseExpression(),
            };

            body.Add(statement);
        }

        return new Tree(body);
    }

    private Expression ParseExpression()
    {
        return ParseBooleanComparisonExpression();
    }

    #region Expressions

    private Expression ParseBooleanComparisonExpression()
    {
        var left = ParseCompareExpression();

        while (Current().value
               is "&&" or "||"
               or "and" or "or")
        {
            var operation = Move().value;

            var right = ParseCompareExpression();

            left = new BooleanJointExpression(left, right, operation);
        }

        return left;

        Expression ParseCompareExpression()
        {
            var _left = ParseNotCompareExpression();

            while (Current().value
                   is "==" or "!=" or ">="
                   or "<=" or ">" or "<" or "is")
            {
                var _operation = Move().value;

                var _right = ParseNotCompareExpression();

                _left = new BooleanComparisonExpression(_left, _right, _operation);
            }

            return _left;
        }

        Expression ParseNotCompareExpression()
        {
            if (Current().value is not ("!" or "not")) return ParseBinaryExpression();

            Move();
            var expression = ParseBinaryExpression();
            return new BooleanReversExpression(expression);
        }
    }

    private Expression ParseBinaryExpression()
    {
        return ParseAdditiveExpression();

        Expression ParseAdditiveExpression()
        {
            var left = ParseMultiplicativeExpression();

            while (Current().value is "+" or "-")
            {
                var binaryOperator = Move().value;
                var right = ParseMultiplicativeExpression();

                left = new BinaryExpression(left, right, binaryOperator);
            }

            return left;
        }

        Expression ParseMultiplicativeExpression()
        {
            var left = ParsePrimaryExpression();

            while (Current().value is "/" or "*" or "%")
            {
                var binaryOperator = Move().value;
                var right = ParsePrimaryExpression();

                left = new BinaryExpression(left, right, binaryOperator);
            }

            return left;
        }
    }

    private Expression ParsePrimaryExpression()
    {
        Expression expression;
        var token = Current();

        try
        {
            switch (token.kind)
            {
                case TokenKind.OpenParen:
                    Move();
                    expression = ParseExpression();
                    MoveAndExpect(TokenKind.CloseParen);
                    break;

                case TokenKind.Identifier:
                    expression = new IdentifierExpression(Move().value);
                    break;

                case TokenKind.Boolean:
                    expression = new BooleanExpression(bool.Parse(Move().value));
                    break;

                case TokenKind.Null:
                    Move();
                    expression = new NullExpression();
                    break;

                case TokenKind.String:
                    expression = new StringExpression(Move().value);
                    break;

                case TokenKind.Char:
                    expression = new CharExpression(Move().value[0]);
                    break;

                #region Numeric

                case TokenKind.Integer:
                    expression = new IntegerExpression(int.Parse(Move().value));
                    break;

                case TokenKind.Float:
                    expression = new FloatExpression(float.Parse(Move().value));
                    break;

                case TokenKind.Long:
                    expression = new LongExpression(long.Parse(Move().value));
                    break;

                case TokenKind.Double:
                    expression = new DoubleExpression(double.Parse(Move().value));
                    break;

                case TokenKind.Byte:
                    expression = new ByteExpression(byte.Parse(Move().value));
                    break;

                #endregion

                // Parse negative and positive numbers with signs like -90 , +7
                case TokenKind.BinaryOperator:
                {
                    var signToken = Move();
                    var sign = signToken.value;

                    if (sign is not ("+" or "-"))
                        throw new ParserException($"Invalid numeric sign: '{sign}'", signToken);

                    var numExpr = (ObjectExpression)ParsePrimaryExpression();
                    var modifier = sign == "-" ? -1 : 1;

                    return numExpr.kind switch
                    {
                        NodeKind.Integer => new IntegerExpression((int)numExpr.GetValue()! * modifier),
                        NodeKind.Float => new FloatExpression((float)numExpr.GetValue()! * modifier),
                        NodeKind.Long => new LongExpression((long)numExpr.GetValue()! * modifier),
                        NodeKind.Double => new DoubleExpression((double)numExpr.GetValue()! * modifier),
                        _ => throw new ParserException($"Expected number value. after '{sign}' sign.")
                    };
                }

                default:
                    throw new ParserException("Invalid or unimplemented token", Current());
            }
        }
        catch (FormatException) // Like int.parse float.parse
        {
            throw new ParserException(
                $"[Parser] Cannot parsing this value [{token.value}] to token of type [{token.kind}]: at [{token.start}, {token.end}]"
            );
        }
        catch (Exception)
        {
            throw new ParserException($"[Parser] Unexpected error while parsing token: [{token}]");
        }

        return expression;
    }

    #endregion

    #region Tools

    /// <summary>
    /// Check if the tokens list is empty
    /// </summary>
    private bool IsEmpty() => tokens.Count == 0 || Current().kind == TokenKind.EndOfFile;

    /// <summary>
    /// Get current active token
    /// </summary>
    private Token Current() => tokens[0];

    /// <summary>
    /// Move one token
    /// </summary>
    private Token Move()
    {
        var item = tokens[0];
        tokens.RemoveAt(0);
        return item;
    }

    /// <summary>
    /// Move one token if match kind
    /// </summary>
    /// <param name="kind">Expected kind</param>
    private Token MoveAndExpect(TokenKind kind)
    {
        if (IsEmpty())
        {
            throw new ParserException($"Expect [{kind}] but file is end.");
        }

        if (Current().kind == kind) return Move();

        throw new ParserException(
            $"Expected [{kind}] but found [{Current().kind}]", Current());
    }

    #endregion
}