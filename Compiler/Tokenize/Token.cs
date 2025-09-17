namespace HorizonCompiler.Tokenize;

public record Token(TokenKind kind, string value, Location start, Location end);