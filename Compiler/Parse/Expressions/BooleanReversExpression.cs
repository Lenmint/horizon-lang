using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Parse.Expressions;

public class BooleanReversExpression(Expression expression) : Expression(NodeKind.BooleanReversExpression)
{
    public readonly Expression expression = expression;
}