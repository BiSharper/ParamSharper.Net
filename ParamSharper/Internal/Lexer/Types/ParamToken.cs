using BiSharper.LanguageUtils;

namespace ParamSharper.Internal.Lexer.Types;

public readonly struct ParamToken : IBisToken<ParamLexeme>
{
    public required ParamLexeme Type { get; init; }
    public required string Text { get; init; }
    public required int Line { get; init; }
    public required int Column { get; init; }
}