﻿namespace ParamSharper.Internal.Lexer;

public readonly ref struct ParamToken
{
    public required ParamLexeme Type { get; init; }
    public required string Text { get; init; }
    public required int Line { get; init; }
    public required int Column { get; init; }
}