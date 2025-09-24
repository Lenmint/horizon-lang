using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Parse.Expressions;

public class ObjectExpression(object? value, NodeKind kind) : Expression(kind)
{
    public virtual object? GetValue()
    {
        return value;
    }
}

public class NullExpression() : ObjectExpression(null, NodeKind.Null);

public class IdentifierExpression(string value) : ObjectExpression(value, NodeKind.Identifier)
{
    public readonly string value = value;
}

#region Numeric

public class NumberExpression(object value, NodeKind kind) : ObjectExpression(value, kind);

public class IntegerExpression(int value) : NumberExpression(value, NodeKind.Integer);

public class LongExpression(long value) : NumberExpression(value, NodeKind.Long);

public class FloatExpression(float value) : NumberExpression(value, NodeKind.Float);

public class DoubleExpression(double value) : NumberExpression(value, NodeKind.Double);

public class ByteExpression(byte value) : ObjectExpression(value, NodeKind.Byte);

#endregion

public class StringExpression(string value) : ObjectExpression(value, NodeKind.String);

public class CharExpression(char value) : ObjectExpression(value, NodeKind.Char);

public class BooleanExpression(bool value) : ObjectExpression(value, NodeKind.Boolean);