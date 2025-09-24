namespace HorizonCompiler.Parse.Core;

public enum NodeKind
{
    Tree,
    Void,

    // == Statements
    ScopeStatement,
    VariableStatement,
    
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