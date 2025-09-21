using HorizonCompiler.Parse.Core;

namespace HorizonCompiler.Evaluate.Values;

public class Value(object? value, NodeKind kind)
{
    public readonly object? value = value;
    public readonly NodeKind kind = kind;
}

public class NullValue() : Value(null, NodeKind.Null);

public class BooleanValue(bool value) : Value(value, NodeKind.Boolean)
{
    public new readonly bool value = value;
}

public class StringValue(string value): Value(value, NodeKind.String)
{
    public new readonly string value = value;
}

#region Numeric

public class NumberValue(object value, NodeKind kind) : Value(value, kind);

public class CharValue(char value): NumberValue(value, NodeKind.Char)
{
    public new readonly char value = value;
}

public class IntegerValue(int value) : NumberValue(value, NodeKind.Integer)
{
    public new readonly int value = value;
}

public class FloatValue(float value) : NumberValue(value, NodeKind.Float)
{
    public new readonly float value = value;
}

public class LongValue(long value) : NumberValue(value, NodeKind.Long)
{
    public new readonly long value = value;
}

public class DoubleValue(double value) : NumberValue(value, NodeKind.Double)
{
    public new readonly double value = value;
}

public class ByteValue(byte value) : NumberValue(value, NodeKind.Byte)
{
    public new readonly byte value = value;
}

#endregion