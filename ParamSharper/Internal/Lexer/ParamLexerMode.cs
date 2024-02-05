namespace ParamSharper.Internal.Lexer;

[Flags]
internal enum ParamLexerMode : byte
{
    Syntax = 1,
    PreProcessor = 2,
    Value = 4,
}