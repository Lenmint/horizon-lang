namespace HorizonCompiler.Parse.Core;

public class Statement(NodeKind kind)
{
    public readonly NodeKind kind = kind;
}

public class Expression(NodeKind kind) : Statement(kind);