using HorizonCompiler.Evaluate.Values;
using HorizonCompiler.Parse.Core;
using HorizonCompiler.Parse.Expressions;

namespace HorizonCompiler.Evaluate;

public class Interpreter
{
    public Value Evaluate(Statement statement)
    {
        switch (statement.kind)
        {
            case NodeKind.Null:
                return new NullValue();

            case NodeKind.Byte:
                return new ByteValue((byte)((ByteExpression)statement).value!);

            case NodeKind.Integer:
                return new IntegerValue((int)((IntegerExpression)statement).value!);

            case NodeKind.Long:
                return new LongValue((long)((LongExpression)statement).value!);

            case NodeKind.Float:
                return new FloatValue((float)((FloatExpression)statement).value!);

            case NodeKind.Double:
                return new DoubleValue((double)((DoubleExpression)statement).value!);

            case NodeKind.Char:
                return new CharValue((char)((CharExpression)statement).value!);

            case NodeKind.String:
                return new StringValue((string)((StringExpression)statement).value!);

            case NodeKind.Boolean:
                return new BooleanValue((bool)((BooleanExpression)statement).value!);

            #region Compliex Expressions

            case NodeKind.BinaryExpression:
                return EvaluateBinaryExpression((statement as BinaryExpression)!);

            #endregion
        }

        Console.WriteLine($"This AST Node [{statement.kind}] is not implemented yet");
        Environment.Exit(1);
        throw new Exception();
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

                case NodeKind.Byte when right.kind == NodeKind.Float:
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

                case NodeKind.Byte when right.kind == NodeKind.Long:
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

                case NodeKind.Byte when right.kind == NodeKind.Double:
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

                case NodeKind.Char when right.kind == NodeKind.Float:
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

                case NodeKind.Char when right.kind == NodeKind.Long:
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

                case NodeKind.Char when right.kind == NodeKind.Double:
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

        Console.WriteLine($"Cannot do operation [{expression.operation}] for those types [{left.kind}, {right.kind}]");
        Environment.Exit(1);
        throw new Exception();
    }
}