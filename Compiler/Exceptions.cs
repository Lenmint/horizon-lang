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