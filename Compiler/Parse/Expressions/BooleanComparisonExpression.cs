using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Parse.Expressions;

public class BooleanComparisonExpression(Expression left, Expression right, string operation) : Expression(NodeKind.BooleanComparisonExpression)
{
    public readonly Expression left = left;
    public readonly Expression right = right;
    public readonly string operation = operation;
}