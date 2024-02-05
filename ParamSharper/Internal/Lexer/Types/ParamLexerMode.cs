namespace ParamSharper.Internal.Lexer.Types;

[Flags]
internal enum ParamLexerMode : byte
{
    Syntax = 1,
    Value = 2,
    PreProcessor = 4,
}