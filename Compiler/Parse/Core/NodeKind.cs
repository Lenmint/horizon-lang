namespace HorizonCompiler.Parse.Core;

public enum NodeKind
{
    Tree,

    // == Expressions
    BinaryExpression,
    BooleanComparisonExpression,
    BooleanJointExpression,
    BooleanReversExpression,

    Identifier,
    Integer,
    Long,
    Float,
    Double,
    Byte,
    Null,
    Boolean,
    String,
    Char,
}