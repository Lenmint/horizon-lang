using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Parse.Statements;

public class Tree(List<Statement> body) : Statement(NodeKind.Tree)
{
    public readonly List<Statement> body = body;
}