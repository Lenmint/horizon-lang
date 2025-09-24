using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Parse.Statements;

public class ScopeStatement(List<Statement> statements) : Statement(NodeKind.ScopeStatement)
{
    public readonly List<Statement> statements = statements;
}