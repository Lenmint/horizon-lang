namespace HorizonCompiler.Tokenize;

public enum TokenKind
{
    Const, //                   const
    Var, //                     var
    Identifier, //              x y z foo bar ...

    Null, //                    null
    Void, //                    void
    Byte, //                    0b 100b 255B
    Integer, //                 10 -95 1995 999i
    Float, //                   10f .15f 0.166f 19.1F
    Double, //                  10d .45d 78D
    Long, //                    -10l 546l 78944013500L
    Char, //                    'c' 'y' 'c' 'l' 'e' '1' 'A' '9'
    String, //                  "Hello World"
    Boolean, //                 true false

    EqualsOperator, //          =
    BinaryOperator, //          + - * / %
    AssignmentOperator, //      += -= *= /= %= ++ --
    CompareOperator, //         == != <= >= < >
    JointOperator, //           && || and or
    NotOperator, //             ! not

    AtSign, //                  @
    DollarSign, //              $
    QuestionMark, //            ?
    Ampersand, //               &

    Comma, //                   ,
    Dot, //                     .
    Colon, //                   :
    DoubleColon, //             ::
    Semicolon, //               ;

    OpenParen, //               (
    CloseParen, //              )
    OpenBrace, //               {
    CloseBrace, //              }
    OpenBracket, //             [
    CloseBracket, //            ]

    EndOfFile, //               End of the file
    EmptyToken, //              Like comments
}