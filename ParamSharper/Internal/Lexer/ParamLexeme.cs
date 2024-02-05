namespace ParamSharper.Internal.Lexer;

public enum ParamLexeme : byte
{
    Preprocessor, Class, Delete, Enum, Identifier, Assign, AddAssign, SubAssign, LeftSquare,
    RightSquare, LeftCurly, RightCurly, Primitive, Whitespace, Comma, Semicolon, Colon, Invalid
}