using HorizonCompiler.Evaluate.Values;
using HorizonCompiler.Parse.Core;
using HorizonCompiler.Parse.Expressions;

namespace HorizonCompiler.Evaluate;

public class Interpreter
{
    private const float EPSILON_FLOAT = 0.0001f;
    private const float EPSILON_FLOAT_SINGLE = 1e-6f;
    private const double EPSILON_DOUBLE = 1e-6;
    private const double EPSILON_DOUBLE_SINGLE = 1e-12d;

    public Value Evaluate(Statement statement)
    {
        switch (statement.kind)
        {
            case NodeKind.Null:
                return new NullValue();

            case NodeKind.Byte:
                return new ByteValue((byte)((ObjectExpression)statement).GetValue()!);

            case NodeKind.Integer:
                return new IntegerValue((int)((ObjectExpression)statement).GetValue()!);

            case NodeKind.Long:
                return new LongValue((long)((ObjectExpression)statement).GetValue()!);

            case NodeKind.Float:
                return new FloatValue((float)((ObjectExpression)statement).GetValue()!);

            case NodeKind.Double:
                return new DoubleValue((double)((ObjectExpression)statement).GetValue()!);

            case NodeKind.Char:
                return new CharValue((char)((ObjectExpression)statement).GetValue()!);

            case NodeKind.String:
                return new StringValue((string)((ObjectExpression)statement).GetValue()!);

            case NodeKind.Boolean:
                return new BooleanValue((bool)((ObjectExpression)statement).GetValue()!);

            #region Compliex Expressions

            case NodeKind.BinaryExpression:
                return EvaluateBinaryExpression((statement as BinaryExpression)!);

            case NodeKind.BooleanComparisonExpression:
                return EvaluateBooleanComparisonExpression((BooleanComparisonExpression)statement);

            #endregion
        }

        throw new InterpreterException($"This AST Node [{statement.kind}] is not implemented yet");
    }

    public Value EvaluateBinaryExpression(BinaryExpression expression)
    {
        var left = Evaluate(expression.left);
        var right = Evaluate(expression.right);

        if (left is NumberValue && right is NumberValue)
        {
            var value = new object();
            switch (left.kind)
            {
                #region Byte

                case NodeKind.Byte when right.kind == NodeKind.Byte:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (byte)left.value! + (byte)right.value!;
                            break;
                        case "-":
                            value = (byte)left.value! - (byte)right.value!;
                            break;
                        case "*":
                            value = (byte)left.value! * (byte)right.value!;
                            break;
                        case "/":
                            value = (byte)left.value! / (byte)right.value!;
                            break;
                        case "%":
                            value = (byte)left.value! % (byte)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Integer);

                case NodeKind.Byte when right.kind == NodeKind.Integer:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (byte)left.value! + (int)right.value!;
                            break;
                        case "-":
                            value = (byte)left.value! - (int)right.value!;
                            break;
                        case "*":
                            value = (byte)left.value! * (int)right.value!;
                            break;
                        case "/":
                            value = (byte)left.value! / (int)right.value!;
                            break;
                        case "%":
                            value = (byte)left.value! % (int)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Byte when right.kind == NodeKind.Float:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (byte)left.value! + (float)right.value!;
                            break;
                        case "-":
                            value = (byte)left.value! - (float)right.value!;
                            break;
                        case "*":
                            value = (byte)left.value! * (float)right.value!;
                            break;
                        case "/":
                            value = (byte)left.value! / (float)right.value!;
                            break;
                        case "%":
                            value = (byte)left.value! % (float)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Byte when right.kind == NodeKind.Long:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (byte)left.value! + (long)right.value!;
                            break;
                        case "-":
                            value = (byte)left.value! - (long)right.value!;
                            break;
                        case "*":
                            value = (byte)left.value! * (long)right.value!;
                            break;
                        case "/":
                            value = (byte)left.value! / (long)right.value!;
                            break;
                        case "%":
                            value = (byte)left.value! % (long)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Byte when right.kind == NodeKind.Double:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (byte)left.value! + (double)right.value!;
                            break;
                        case "-":
                            value = (byte)left.value! - (double)right.value!;
                            break;
                        case "*":
                            value = (byte)left.value! * (double)right.value!;
                            break;
                        case "/":
                            value = (byte)left.value! / (double)right.value!;
                            break;
                        case "%":
                            value = (byte)left.value! % (double)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Byte when right.kind == NodeKind.Char:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (byte)left.value! + (char)right.value!;
                            break;
                        case "-":
                            value = (byte)left.value! - (char)right.value!;
                            break;
                        case "*":
                            value = (byte)left.value! * (char)right.value!;
                            break;
                        case "/":
                            value = (byte)left.value! / (char)right.value!;
                            break;
                        case "%":
                            value = (byte)left.value! % (char)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Integer);

                #endregion

                #region Integer

                case NodeKind.Integer when right.kind == NodeKind.Byte:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (int)left.value! + (byte)right.value!;
                            break;
                        case "-":
                            value = (int)left.value! - (byte)right.value!;
                            break;
                        case "*":
                            value = (int)left.value! * (byte)right.value!;
                            break;
                        case "/":
                            value = (int)left.value! / (byte)right.value!;
                            break;
                        case "%":
                            value = (int)left.value! % (byte)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Integer);

                case NodeKind.Integer when right.kind == NodeKind.Integer:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (int)left.value! + (int)right.value!;
                            break;
                        case "-":
                            value = (int)left.value! - (int)right.value!;
                            break;
                        case "*":
                            value = (int)left.value! * (int)right.value!;
                            break;
                        case "/":
                            value = (int)left.value! / (int)right.value!;
                            break;
                        case "%":
                            value = (int)left.value! % (int)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Integer);

                case NodeKind.Integer when right.kind == NodeKind.Float:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (int)left.value! + (float)right.value!;
                            break;
                        case "-":
                            value = (int)left.value! - (float)right.value!;
                            break;
                        case "*":
                            value = (int)left.value! * (float)right.value!;
                            break;
                        case "/":
                            value = (int)left.value! / (float)right.value!;
                            break;
                        case "%":
                            value = (int)left.value! % (float)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Float);

                case NodeKind.Integer when right.kind == NodeKind.Long:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (int)left.value! + (long)right.value!;
                            break;
                        case "-":
                            value = (int)left.value! - (long)right.value!;
                            break;
                        case "*":
                            value = (int)left.value! * (long)right.value!;
                            break;
                        case "/":
                            value = (int)left.value! / (long)right.value!;
                            break;
                        case "%":
                            value = (int)left.value! % (long)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Long);

                case NodeKind.Integer when right.kind == NodeKind.Double:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (int)left.value! + (double)right.value!;
                            break;
                        case "-":
                            value = (int)left.value! - (double)right.value!;
                            break;
                        case "*":
                            value = (int)left.value! * (double)right.value!;
                            break;
                        case "/":
                            value = (int)left.value! / (double)right.value!;
                            break;
                        case "%":
                            value = (int)left.value! % (double)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Integer when right.kind == NodeKind.Char:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (int)left.value! + (char)right.value!;
                            break;
                        case "-":
                            value = (int)left.value! - (char)right.value!;
                            break;
                        case "*":
                            value = (int)left.value! * (char)right.value!;
                            break;
                        case "/":
                            value = (int)left.value! / (char)right.value!;
                            break;
                        case "%":
                            value = (int)left.value! % (char)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Integer);

                #endregion

                #region Float

                case NodeKind.Float when right.kind == NodeKind.Byte:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (float)left.value! + (byte)right.value!;
                            break;
                        case "-":
                            value = (float)left.value! - (byte)right.value!;
                            break;
                        case "*":
                            value = (float)left.value! * (byte)right.value!;
                            break;
                        case "/":
                            value = (float)left.value! / (byte)right.value!;
                            break;
                        case "%":
                            value = (float)left.value! % (byte)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Float);

                case NodeKind.Float when right.kind == NodeKind.Integer:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (float)left.value! + (int)right.value!;
                            break;
                        case "-":
                            value = (float)left.value! - (int)right.value!;
                            break;
                        case "*":
                            value = (float)left.value! * (int)right.value!;
                            break;
                        case "/":
                            value = (float)left.value! / (int)right.value!;
                            break;
                        case "%":
                            value = (float)left.value! % (int)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Float);

                case NodeKind.Float when right.kind == NodeKind.Float:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (float)left.value! + (float)right.value!;
                            break;
                        case "-":
                            value = (float)left.value! - (float)right.value!;
                            break;
                        case "*":
                            value = (float)left.value! * (float)right.value!;
                            break;
                        case "/":
                            value = (float)left.value! / (float)right.value!;
                            break;
                        case "%":
                            value = (float)left.value! % (float)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Float);

                case NodeKind.Float when right.kind == NodeKind.Long:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (float)left.value! + (long)right.value!;
                            break;
                        case "-":
                            value = (float)left.value! - (long)right.value!;
                            break;
                        case "*":
                            value = (float)left.value! * (long)right.value!;
                            break;
                        case "/":
                            value = (float)left.value! / (long)right.value!;
                            break;
                        case "%":
                            value = (float)left.value! % (long)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Float);

                case NodeKind.Float when right.kind == NodeKind.Double:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (float)left.value! + (double)right.value!;
                            break;
                        case "-":
                            value = (float)left.value! - (double)right.value!;
                            break;
                        case "*":
                            value = (float)left.value! * (double)right.value!;
                            break;
                        case "/":
                            value = (float)left.value! / (double)right.value!;
                            break;
                        case "%":
                            value = (float)left.value! % (double)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Float when right.kind == NodeKind.Char:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (float)left.value! + (char)right.value!;
                            break;
                        case "-":
                            value = (float)left.value! - (char)right.value!;
                            break;
                        case "*":
                            value = (float)left.value! * (char)right.value!;
                            break;
                        case "/":
                            value = (float)left.value! / (char)right.value!;
                            break;
                        case "%":
                            value = (float)left.value! % (char)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Float);

                #endregion

                #region Long

                case NodeKind.Long when right.kind == NodeKind.Byte:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (long)left.value! + (byte)right.value!;
                            break;
                        case "-":
                            value = (long)left.value! - (byte)right.value!;
                            break;
                        case "*":
                            value = (long)left.value! * (byte)right.value!;
                            break;
                        case "/":
                            value = (long)left.value! / (byte)right.value!;
                            break;
                        case "%":
                            value = (long)left.value! % (byte)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Long);

                case NodeKind.Long when right.kind == NodeKind.Integer:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (long)left.value! + (int)right.value!;
                            break;
                        case "-":
                            value = (long)left.value! - (int)right.value!;
                            break;
                        case "*":
                            value = (long)left.value! * (int)right.value!;
                            break;
                        case "/":
                            value = (long)left.value! / (int)right.value!;
                            break;
                        case "%":
                            value = (long)left.value! % (int)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Long);

                case NodeKind.Long when right.kind == NodeKind.Float:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (long)left.value! + (float)right.value!;
                            break;
                        case "-":
                            value = (long)left.value! - (float)right.value!;
                            break;
                        case "*":
                            value = (long)left.value! * (float)right.value!;
                            break;
                        case "/":
                            value = (long)left.value! / (float)right.value!;
                            break;
                        case "%":
                            value = (long)left.value! % (float)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Float);

                case NodeKind.Long when right.kind == NodeKind.Long:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (long)left.value! + (long)right.value!;
                            break;
                        case "-":
                            value = (long)left.value! - (long)right.value!;
                            break;
                        case "*":
                            value = (long)left.value! * (long)right.value!;
                            break;
                        case "/":
                            value = (long)left.value! / (long)right.value!;
                            break;
                        case "%":
                            value = (long)left.value! % (long)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Long);

                case NodeKind.Long when right.kind == NodeKind.Double:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (long)left.value! + (double)right.value!;
                            break;
                        case "-":
                            value = (long)left.value! - (double)right.value!;
                            break;
                        case "*":
                            value = (long)left.value! * (double)right.value!;
                            break;
                        case "/":
                            value = (long)left.value! / (double)right.value!;
                            break;
                        case "%":
                            value = (long)left.value! % (double)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Long when right.kind == NodeKind.Char:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (long)left.value! + (char)right.value!;
                            break;
                        case "-":
                            value = (long)left.value! - (char)right.value!;
                            break;
                        case "*":
                            value = (long)left.value! * (char)right.value!;
                            break;
                        case "/":
                            value = (long)left.value! / (char)right.value!;
                            break;
                        case "%":
                            value = (long)left.value! % (char)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Long);

                #endregion

                #region Double

                case NodeKind.Double when right.kind == NodeKind.Byte:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (double)left.value! + (byte)right.value!;
                            break;
                        case "-":
                            value = (double)left.value! - (byte)right.value!;
                            break;
                        case "*":
                            value = (double)left.value! * (byte)right.value!;
                            break;
                        case "/":
                            value = (double)left.value! / (byte)right.value!;
                            break;
                        case "%":
                            value = (double)left.value! % (byte)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Double when right.kind == NodeKind.Integer:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (double)left.value! + (int)right.value!;
                            break;
                        case "-":
                            value = (double)left.value! - (int)right.value!;
                            break;
                        case "*":
                            value = (double)left.value! * (int)right.value!;
                            break;
                        case "/":
                            value = (double)left.value! / (int)right.value!;
                            break;
                        case "%":
                            value = (double)left.value! % (int)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Double when right.kind == NodeKind.Float:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (double)left.value! + (float)right.value!;
                            break;
                        case "-":
                            value = (double)left.value! - (float)right.value!;
                            break;
                        case "*":
                            value = (double)left.value! * (float)right.value!;
                            break;
                        case "/":
                            value = (double)left.value! / (float)right.value!;
                            break;
                        case "%":
                            value = (double)left.value! % (float)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Double when right.kind == NodeKind.Long:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (double)left.value! + (long)right.value!;
                            break;
                        case "-":
                            value = (double)left.value! - (long)right.value!;
                            break;
                        case "*":
                            value = (double)left.value! * (long)right.value!;
                            break;
                        case "/":
                            value = (double)left.value! / (long)right.value!;
                            break;
                        case "%":
                            value = (double)left.value! % (long)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Double when right.kind == NodeKind.Double:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (double)left.value! + (double)right.value!;
                            break;
                        case "-":
                            value = (double)left.value! - (double)right.value!;
                            break;
                        case "*":
                            value = (double)left.value! * (double)right.value!;
                            break;
                        case "/":
                            value = (double)left.value! / (double)right.value!;
                            break;
                        case "%":
                            value = (double)left.value! % (double)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Double when right.kind == NodeKind.Char:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (double)left.value! + (char)right.value!;
                            break;
                        case "-":
                            value = (double)left.value! - (char)right.value!;
                            break;
                        case "*":
                            value = (double)left.value! * (char)right.value!;
                            break;
                        case "/":
                            value = (double)left.value! / (char)right.value!;
                            break;
                        case "%":
                            value = (double)left.value! % (char)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Integer);

                #endregion

                #region Char

                case NodeKind.Char when right.kind == NodeKind.Byte:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (char)left.value! + (byte)right.value!;
                            break;
                        case "-":
                            value = (char)left.value! - (byte)right.value!;
                            break;
                        case "*":
                            value = (char)left.value! * (byte)right.value!;
                            break;
                        case "/":
                            value = (char)left.value! / (byte)right.value!;
                            break;
                        case "%":
                            value = (char)left.value! % (byte)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Char when right.kind == NodeKind.Char:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (char)left.value! + (char)right.value!;
                            break;
                        case "-":
                            value = (char)left.value! - (char)right.value!;
                            break;
                        case "*":
                            value = (char)left.value! * (char)right.value!;
                            break;
                        case "/":
                            value = (char)left.value! / (char)right.value!;
                            break;
                        case "%":
                            value = (char)left.value! % (char)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Integer);

                case NodeKind.Char when right.kind == NodeKind.Integer:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (char)left.value! + (int)right.value!;
                            break;
                        case "-":
                            value = (char)left.value! - (int)right.value!;
                            break;
                        case "*":
                            value = (char)left.value! * (int)right.value!;
                            break;
                        case "/":
                            value = (char)left.value! / (int)right.value!;
                            break;
                        case "%":
                            value = (char)left.value! % (int)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Char when right.kind == NodeKind.Float:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (char)left.value! + (float)right.value!;
                            break;
                        case "-":
                            value = (char)left.value! - (float)right.value!;
                            break;
                        case "*":
                            value = (char)left.value! * (float)right.value!;
                            break;
                        case "/":
                            value = (char)left.value! / (float)right.value!;
                            break;
                        case "%":
                            value = (char)left.value! % (float)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Char when right.kind == NodeKind.Long:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (char)left.value! + (long)right.value!;
                            break;
                        case "-":
                            value = (char)left.value! - (long)right.value!;
                            break;
                        case "*":
                            value = (char)left.value! * (long)right.value!;
                            break;
                        case "/":
                            value = (char)left.value! / (long)right.value!;
                            break;
                        case "%":
                            value = (char)left.value! % (long)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                case NodeKind.Char when right.kind == NodeKind.Double:
                    switch (expression.operation)
                    {
                        case "+":
                            value = (char)left.value! + (double)right.value!;
                            break;
                        case "-":
                            value = (char)left.value! - (double)right.value!;
                            break;
                        case "*":
                            value = (char)left.value! * (double)right.value!;
                            break;
                        case "/":
                            value = (char)left.value! / (double)right.value!;
                            break;
                        case "%":
                            value = (char)left.value! % (double)right.value!;
                            break;
                    }

                    return new NumberValue(value, NodeKind.Double);

                #endregion
            }
        }

        // Operations for non-numbers like String, Char, etc...
        else
        {
            if (expression.operation == "+")
            {
                switch (left)
                {
                    // String + String => String
                    case StringValue when right is StringValue:
                        return new StringValue((string)left.value! + (string)right.value!);
                    // String + Char => String
                    case StringValue when right is CharValue:
                        return new StringValue((string)left.value! + (char)right.value!);
                    // Char + String => String
                    case CharValue when right is StringValue:
                        return new StringValue((char)left.value! + (string)right.value!);

                    // String + Int => String
                    case StringValue when right is NumberValue && right.kind is NodeKind.Integer:
                        return new StringValue((string)left.value! + (int)right.value!);
                    // String + Long => String
                    case StringValue when right is NumberValue && right.kind is NodeKind.Long:
                        return new StringValue((string)left.value! + (long)right.value!);
                    // Int + String => String
                    case NumberValue when left.kind is NodeKind.Integer && right is StringValue:
                        return new StringValue((int)left.value! + (string)right.value!);
                    // Long + String => String
                    case NumberValue when left.kind is NodeKind.Long && right is StringValue:
                        return new StringValue((long)left.value! + (string)right.value!);

                    // String + Float => String
                    case StringValue when right is NumberValue && right.kind is NodeKind.Float:
                        return new StringValue((string)left.value! + (float)right.value!);
                    // String + Double => String
                    case StringValue when right is NumberValue && right.kind is NodeKind.Double:
                        return new StringValue((string)left.value! + (double)right.value!);
                    // Float + String => String
                    case NumberValue when left.kind is NodeKind.Float && right is StringValue:
                        return new StringValue((float)left.value! + (string)right.value!);
                    // Double + String => String
                    case NumberValue when left.kind is NodeKind.Double && right is StringValue:
                        return new StringValue((double)left.value! + (string)right.value!);
                }
            }
        }

        throw new InterpreterException($"Cannot do operation [{expression.operation}] for those types [{left.kind}, {right.kind}]");
    }

    public BooleanValue EvaluateBooleanComparisonExpression(BooleanComparisonExpression expression)
    {
        var left = Evaluate(expression.left);
        var right = Evaluate(expression.right);

        #region Null Check

        if (left is NullValue)
        {
            switch (expression.operation)
            {
                case "==":
                    return new BooleanValue(null == right.value);
                case "!=":
                    return new BooleanValue(null != right.value);
            }
        }
        else if (right is NullValue)
        {
            switch (expression.operation)
            {
                case "==":
                    return new BooleanValue(left.value == null);
                case "!=":
                    return new BooleanValue(left.value != null);
            }
        }

        #endregion

        switch (left)
        {
            case BooleanValue when right is BooleanValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((bool)left.value! == (bool)right.value!);
                    case "!=":
                        return new BooleanValue((bool)left.value! != (bool)right.value!);
                }

                break;

            #region Byte

            case ByteValue when right is ByteValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((byte)left.value! == (byte)right.value!);
                    case "!=":
                        return new BooleanValue((byte)left.value! != (byte)right.value!);
                    case ">=":
                        return new BooleanValue((byte)left.value! >= (byte)right.value!);
                    case "<=":
                        return new BooleanValue((byte)left.value! <= (byte)right.value!);
                    case ">":
                        return new BooleanValue((byte)left.value! > (byte)right.value!);
                    case "<":
                        return new BooleanValue((byte)left.value! < (byte)right.value!);
                }

                break;

            case ByteValue when right is IntegerValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((byte)left.value! == (int)right.value!);
                    case "!=":
                        return new BooleanValue((byte)left.value! != (int)right.value!);
                    case ">=":
                        return new BooleanValue((byte)left.value! >= (int)right.value!);
                    case "<=":
                        return new BooleanValue((byte)left.value! <= (int)right.value!);
                    case ">":
                        return new BooleanValue((byte)left.value! > (int)right.value!);
                    case "<":
                        return new BooleanValue((byte)left.value! < (int)right.value!);
                }

                break;

            case ByteValue when right is FloatValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((byte)left.value! - (float)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((byte)left.value! - (float)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((byte)left.value! >= (float)right.value!);
                    case "<=":
                        return new BooleanValue((byte)left.value! <= (float)right.value!);
                    case ">":
                        return new BooleanValue((byte)left.value! > (float)right.value!);
                    case "<":
                        return new BooleanValue((byte)left.value! < (float)right.value!);
                }

                break;

            case ByteValue when right is LongValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((byte)left.value! == (long)right.value!);
                    case "!=":
                        return new BooleanValue((byte)left.value! != (long)right.value!);
                    case ">=":
                        return new BooleanValue((byte)left.value! >= (long)right.value!);
                    case "<=":
                        return new BooleanValue((byte)left.value! <= (long)right.value!);
                    case ">":
                        return new BooleanValue((byte)left.value! > (long)right.value!);
                    case "<":
                        return new BooleanValue((byte)left.value! < (long)right.value!);
                }

                break;

            case ByteValue when right is DoubleValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((byte)left.value! - (double)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((byte)left.value! - (double)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((byte)left.value! >= (double)right.value!);
                    case "<=":
                        return new BooleanValue((byte)left.value! <= (double)right.value!);
                    case ">":
                        return new BooleanValue((byte)left.value! > (double)right.value!);
                    case "<":
                        return new BooleanValue((byte)left.value! < (double)right.value!);
                }

                break;

            case ByteValue when right is CharValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((byte)left.value! == (char)right.value!);
                    case "!=":
                        return new BooleanValue((byte)left.value! != (char)right.value!);
                    case ">=":
                        return new BooleanValue((byte)left.value! >= (char)right.value!);
                    case "<=":
                        return new BooleanValue((byte)left.value! <= (char)right.value!);
                    case ">":
                        return new BooleanValue((byte)left.value! > (char)right.value!);
                    case "<":
                        return new BooleanValue((byte)left.value! < (char)right.value!);
                }

                break;

            #endregion

            #region Interger

            case IntegerValue when right is ByteValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((int)left.value! == (byte)right.value!);
                    case "!=":
                        return new BooleanValue((int)left.value! != (byte)right.value!);
                    case ">=":
                        return new BooleanValue((int)left.value! >= (byte)right.value!);
                    case "<=":
                        return new BooleanValue((int)left.value! <= (byte)right.value!);
                    case ">":
                        return new BooleanValue((int)left.value! > (byte)right.value!);
                    case "<":
                        return new BooleanValue((int)left.value! < (byte)right.value!);
                }

                break;

            case IntegerValue when right is IntegerValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((int)left.value! == (int)right.value!);
                    case "!=":
                        return new BooleanValue((int)left.value! != (int)right.value!);
                    case ">=":
                        return new BooleanValue((int)left.value! >= (int)right.value!);
                    case "<=":
                        return new BooleanValue((int)left.value! <= (int)right.value!);
                    case ">":
                        return new BooleanValue((int)left.value! > (int)right.value!);
                    case "<":
                        return new BooleanValue((int)left.value! < (int)right.value!);
                }

                break;

            case IntegerValue when right is FloatValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((int)left.value! - (float)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((int)left.value! - (float)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((int)left.value! >= (float)right.value!);
                    case "<=":
                        return new BooleanValue((int)left.value! <= (float)right.value!);
                    case ">":
                        return new BooleanValue((int)left.value! > (float)right.value!);
                    case "<":
                        return new BooleanValue((int)left.value! < (float)right.value!);
                }

                break;

            case IntegerValue when right is LongValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((int)left.value! == (long)right.value!);
                    case "!=":
                        return new BooleanValue((int)left.value! != (long)right.value!);
                    case ">=":
                        return new BooleanValue((int)left.value! >= (long)right.value!);
                    case "<=":
                        return new BooleanValue((int)left.value! <= (long)right.value!);
                    case ">":
                        return new BooleanValue((int)left.value! > (long)right.value!);
                    case "<":
                        return new BooleanValue((int)left.value! < (long)right.value!);
                }

                break;

            case IntegerValue when right is DoubleValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((int)left.value! - (double)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((int)left.value! - (double)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((int)left.value! >= (double)right.value!);
                    case "<=":
                        return new BooleanValue((int)left.value! <= (double)right.value!);
                    case ">":
                        return new BooleanValue((int)left.value! > (double)right.value!);
                    case "<":
                        return new BooleanValue((int)left.value! < (double)right.value!);
                }

                break;

            case IntegerValue when right is CharValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((int)left.value! == (char)right.value!);
                    case "!=":
                        return new BooleanValue((int)left.value! != (char)right.value!);
                    case ">=":
                        return new BooleanValue((int)left.value! >= (char)right.value!);
                    case "<=":
                        return new BooleanValue((int)left.value! <= (char)right.value!);
                    case ">":
                        return new BooleanValue((int)left.value! > (char)right.value!);
                    case "<":
                        return new BooleanValue((int)left.value! < (char)right.value!);
                }

                break;

            #endregion

            #region Float

            case FloatValue when right is ByteValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((float)left.value! - (byte)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((float)left.value! - (byte)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((float)left.value! >= (byte)right.value!);
                    case "<=":
                        return new BooleanValue((float)left.value! <= (byte)right.value!);
                    case ">":
                        return new BooleanValue((float)left.value! > (byte)right.value!);
                    case "<":
                        return new BooleanValue((float)left.value! < (byte)right.value!);
                }

                break;

            case FloatValue when right is IntegerValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((float)left.value! - (int)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((float)left.value! - (int)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((float)left.value! >= (int)right.value!);
                    case "<=":
                        return new BooleanValue((float)left.value! <= (int)right.value!);
                    case ">":
                        return new BooleanValue((float)left.value! > (int)right.value!);
                    case "<":
                        return new BooleanValue((float)left.value! < (int)right.value!);
                }

                break;

            case FloatValue when right is FloatValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((float)left.value! - (float)right.value!) <
                                                EPSILON_FLOAT_SINGLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((float)left.value! - (float)right.value!) >
                                                EPSILON_FLOAT_SINGLE);
                    case ">=":
                        return new BooleanValue((float)left.value! >= (float)right.value!);
                    case "<=":
                        return new BooleanValue((float)left.value! <= (float)right.value!);
                    case ">":
                        return new BooleanValue((float)left.value! > (float)right.value!);
                    case "<":
                        return new BooleanValue((float)left.value! < (float)right.value!);
                }

                break;

            case FloatValue when right is LongValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((float)left.value! - (long)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((float)left.value! - (long)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((float)left.value! >= (long)right.value!);
                    case "<=":
                        return new BooleanValue((float)left.value! <= (long)right.value!);
                    case ">":
                        return new BooleanValue((float)left.value! > (long)right.value!);
                    case "<":
                        return new BooleanValue((float)left.value! < (long)right.value!);
                }

                break;

            case FloatValue when right is DoubleValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((float)left.value! - (double)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((float)left.value! - (double)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((float)left.value! >= (double)right.value!);
                    case "<=":
                        return new BooleanValue((float)left.value! <= (double)right.value!);
                    case ">":
                        return new BooleanValue((float)left.value! > (double)right.value!);
                    case "<":
                        return new BooleanValue((float)left.value! < (double)right.value!);
                }

                break;

            case FloatValue when right is CharValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((float)left.value! - (char)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((float)left.value! - (char)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((float)left.value! >= (char)right.value!);
                    case "<=":
                        return new BooleanValue((float)left.value! <= (char)right.value!);
                    case ">":
                        return new BooleanValue((float)left.value! > (char)right.value!);
                    case "<":
                        return new BooleanValue((float)left.value! < (char)right.value!);
                }

                break;

            #endregion

            #region Long

            case LongValue when right is ByteValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((long)left.value! == (byte)right.value!);
                    case "!=":
                        return new BooleanValue((long)left.value! != (byte)right.value!);
                    case ">=":
                        return new BooleanValue((long)left.value! >= (byte)right.value!);
                    case "<=":
                        return new BooleanValue((long)left.value! <= (byte)right.value!);
                    case ">":
                        return new BooleanValue((long)left.value! > (byte)right.value!);
                    case "<":
                        return new BooleanValue((long)left.value! < (byte)right.value!);
                }

                break;

            case LongValue when right is IntegerValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((long)left.value! == (int)right.value!);
                    case "!=":
                        return new BooleanValue((long)left.value! != (int)right.value!);
                    case ">=":
                        return new BooleanValue((long)left.value! >= (int)right.value!);
                    case "<=":
                        return new BooleanValue((long)left.value! <= (int)right.value!);
                    case ">":
                        return new BooleanValue((long)left.value! > (int)right.value!);
                    case "<":
                        return new BooleanValue((long)left.value! < (int)right.value!);
                }

                break;

            case LongValue when right is FloatValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((long)left.value! - (float)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((long)left.value! - (float)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((long)left.value! >= (float)right.value!);
                    case "<=":
                        return new BooleanValue((long)left.value! <= (float)right.value!);
                    case ">":
                        return new BooleanValue((long)left.value! > (float)right.value!);
                    case "<":
                        return new BooleanValue((long)left.value! < (float)right.value!);
                }

                break;

            case LongValue when right is LongValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((long)left.value! == (long)right.value!);
                    case "!=":
                        return new BooleanValue((long)left.value! != (long)right.value!);
                    case ">=":
                        return new BooleanValue((long)left.value! >= (long)right.value!);
                    case "<=":
                        return new BooleanValue((long)left.value! <= (long)right.value!);
                    case ">":
                        return new BooleanValue((long)left.value! > (long)right.value!);
                    case "<":
                        return new BooleanValue((long)left.value! < (long)right.value!);
                }

                break;

            case LongValue when right is DoubleValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((long)left.value! - (double)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((long)left.value! - (double)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((long)left.value! >= (double)right.value!);
                    case "<=":
                        return new BooleanValue((long)left.value! <= (double)right.value!);
                    case ">":
                        return new BooleanValue((long)left.value! > (double)right.value!);
                    case "<":
                        return new BooleanValue((long)left.value! < (double)right.value!);
                }

                break;

            case LongValue when right is CharValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((long)left.value! == (char)right.value!);
                    case "!=":
                        return new BooleanValue((long)left.value! != (char)right.value!);
                    case ">=":
                        return new BooleanValue((long)left.value! >= (char)right.value!);
                    case "<=":
                        return new BooleanValue((long)left.value! <= (char)right.value!);
                    case ">":
                        return new BooleanValue((long)left.value! > (char)right.value!);
                    case "<":
                        return new BooleanValue((long)left.value! < (char)right.value!);
                }

                break;

            #endregion

            #region Double

            case DoubleValue when right is ByteValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((double)left.value! - (byte)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((double)left.value! - (byte)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((double)left.value! >= (byte)right.value!);
                    case "<=":
                        return new BooleanValue((double)left.value! <= (byte)right.value!);
                    case ">":
                        return new BooleanValue((double)left.value! > (byte)right.value!);
                    case "<":
                        return new BooleanValue((double)left.value! < (byte)right.value!);
                }

                break;

            case DoubleValue when right is IntegerValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((double)left.value! - (int)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((double)left.value! - (int)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((double)left.value! >= (int)right.value!);
                    case "<=":
                        return new BooleanValue((double)left.value! <= (int)right.value!);
                    case ">":
                        return new BooleanValue((double)left.value! > (int)right.value!);
                    case "<":
                        return new BooleanValue((double)left.value! < (int)right.value!);
                }

                break;

            case DoubleValue when right is FloatValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((double)left.value! - (float)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((double)left.value! - (float)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((double)left.value! >= (float)right.value!);
                    case "<=":
                        return new BooleanValue((double)left.value! <= (float)right.value!);
                    case ">":
                        return new BooleanValue((double)left.value! > (float)right.value!);
                    case "<":
                        return new BooleanValue((double)left.value! < (float)right.value!);
                }

                break;

            case DoubleValue when right is LongValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((double)left.value! - (long)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((double)left.value! - (long)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((double)left.value! >= (long)right.value!);
                    case "<=":
                        return new BooleanValue((double)left.value! <= (long)right.value!);
                    case ">":
                        return new BooleanValue((double)left.value! > (long)right.value!);
                    case "<":
                        return new BooleanValue((double)left.value! < (long)right.value!);
                }

                break;

            case DoubleValue when right is DoubleValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((double)left.value! - (double)right.value!) <
                                                EPSILON_DOUBLE_SINGLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((double)left.value! - (double)right.value!) >
                                                EPSILON_DOUBLE_SINGLE);
                    case ">=":
                        return new BooleanValue((double)left.value! >= (double)right.value!);
                    case "<=":
                        return new BooleanValue((double)left.value! <= (double)right.value!);
                    case ">":
                        return new BooleanValue((double)left.value! > (double)right.value!);
                    case "<":
                        return new BooleanValue((double)left.value! < (double)right.value!);
                }

                break;

            case DoubleValue when right is CharValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((double)left.value! - (char)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((double)left.value! - (char)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((double)left.value! >= (char)right.value!);
                    case "<=":
                        return new BooleanValue((double)left.value! <= (char)right.value!);
                    case ">":
                        return new BooleanValue((double)left.value! > (char)right.value!);
                    case "<":
                        return new BooleanValue((double)left.value! < (char)right.value!);
                }

                break;

            #endregion

            #region Char

            case CharValue when right is ByteValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((char)left.value! == (byte)right.value!);
                    case "!=":
                        return new BooleanValue((char)left.value! != (byte)right.value!);
                    case ">=":
                        return new BooleanValue((char)left.value! >= (byte)right.value!);
                    case "<=":
                        return new BooleanValue((char)left.value! <= (byte)right.value!);
                    case ">":
                        return new BooleanValue((char)left.value! > (byte)right.value!);
                    case "<":
                        return new BooleanValue((char)left.value! < (byte)right.value!);
                }

                break;

            case CharValue when right is IntegerValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((char)left.value! == (int)right.value!);
                    case "!=":
                        return new BooleanValue((char)left.value! != (int)right.value!);
                    case ">=":
                        return new BooleanValue((char)left.value! >= (int)right.value!);
                    case "<=":
                        return new BooleanValue((char)left.value! <= (int)right.value!);
                    case ">":
                        return new BooleanValue((char)left.value! > (int)right.value!);
                    case "<":
                        return new BooleanValue((char)left.value! < (int)right.value!);
                }

                break;

            case CharValue when right is FloatValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((char)left.value! - (float)right.value!) < EPSILON_FLOAT);
                    case "!=":
                        return new BooleanValue(Math.Abs((char)left.value! - (float)right.value!) > EPSILON_FLOAT);
                    case ">=":
                        return new BooleanValue((char)left.value! >= (float)right.value!);
                    case "<=":
                        return new BooleanValue((char)left.value! <= (float)right.value!);
                    case ">":
                        return new BooleanValue((char)left.value! > (float)right.value!);
                    case "<":
                        return new BooleanValue((char)left.value! < (float)right.value!);
                }

                break;

            case CharValue when right is LongValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((char)left.value! == (long)right.value!);
                    case "!=":
                        return new BooleanValue((char)left.value! != (long)right.value!);
                    case ">=":
                        return new BooleanValue((char)left.value! >= (long)right.value!);
                    case "<=":
                        return new BooleanValue((char)left.value! <= (long)right.value!);
                    case ">":
                        return new BooleanValue((char)left.value! > (long)right.value!);
                    case "<":
                        return new BooleanValue((char)left.value! < (long)right.value!);
                }

                break;

            case CharValue when right is DoubleValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue(Math.Abs((char)left.value! - (double)right.value!) < EPSILON_DOUBLE);
                    case "!=":
                        return new BooleanValue(Math.Abs((char)left.value! - (double)right.value!) > EPSILON_DOUBLE);
                    case ">=":
                        return new BooleanValue((char)left.value! >= (double)right.value!);
                    case "<=":
                        return new BooleanValue((char)left.value! <= (double)right.value!);
                    case ">":
                        return new BooleanValue((char)left.value! > (double)right.value!);
                    case "<":
                        return new BooleanValue((char)left.value! < (double)right.value!);
                }

                break;

            case CharValue when right is CharValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((char)left.value! == (char)right.value!);
                    case "!=":
                        return new BooleanValue((char)left.value! != (char)right.value!);
                    case ">=":
                        return new BooleanValue((char)left.value! >= (char)right.value!);
                    case "<=":
                        return new BooleanValue((char)left.value! <= (char)right.value!);
                    case ">":
                        return new BooleanValue((char)left.value! > (char)right.value!);
                    case "<":
                        return new BooleanValue((char)left.value! < (char)right.value!);
                }

                break;

            #endregion

            #region String

            case StringValue when right is StringValue:
                switch (expression.operation)
                {
                    case "==":
                        return new BooleanValue((string)left.value! == (string)right.value!);
                    case "!=":
                        return new BooleanValue((string)left.value! != (string)right.value!);
                }

                break;

            #endregion
        }

        throw new InterpreterException($"Cannot do comparison to those types: [{left.kind}, {right.kind}]");
    }
}