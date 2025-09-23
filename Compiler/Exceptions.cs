using HorizonCompiler.Tokenize;

namespace HorizonCompiler;

public class BaseException : Exception
{
    protected BaseException() { }

    protected BaseException(string message) : base(message) { }

    public override string ToString()
    {
        return Message;
    }
}

public class LexerException(string message, int line, int column) : BaseException(message)
{
    public override string ToString()
    {
        return $"[Error] [Lexer] Location: {line}:{column} " + base.ToString();
    }
}

public class ParserException : BaseException
{
    public readonly Token? token = null;

    public ParserException() : base() { }

    public ParserException(string message) : base(message) { }

    public ParserException(string message, Token token) : base(message)
    {
        this.token = token;
    }

    public override string ToString()
    {
        if (token != null)
            return $"[Error] [Parser] Token: [{token.kind}] at [{token.start}, {token.end}] " + base.ToString();
        return "[Error] [Parser] " + base.ToString();
    }
}