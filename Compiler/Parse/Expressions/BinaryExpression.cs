using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Parse.Expressions;

public class BinaryExpression(Expression left, Expression right, string operation): Expression(NodeKind.BinaryExpression)
{
    public readonly Expression left = left;
    public readonly Expression right = right;
    public readonly string operation = operation;
}