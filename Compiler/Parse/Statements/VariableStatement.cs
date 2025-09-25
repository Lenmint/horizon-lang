using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Parse.Statements;

public class VariableStatement(string identifier, bool dynamic, bool constant, string? type = null, Expression? expression = null) : Statement(NodeKind.VariableStatement)
{
    public readonly bool dynamic = dynamic;
    public readonly bool constant = constant;
    public readonly string? type = type;
    public readonly string identifier = identifier;
    public readonly Expression? expression = expression;
}