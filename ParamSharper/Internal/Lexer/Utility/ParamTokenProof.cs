namespace ParamSharper.Internal.Lexer.Utility;

internal static class ParamTokenProof
{
    public const string KeywordClass = "class";
    public const string KeywordDelete = "delete";
    public const string KeywordEnum = "enum";
    public const string KeywordExecute = "__EXEC";
    public const string KeywordEvaluate = "__EVAL";
    public const string AddAssign = "+=";
    public const string SubAssign = "-=";
    public const string Assign = "=";


    public static bool IsIdentifierChar(int? c) =>
        c is '_' or >= 'A' and <= 'Z' or >= 'a' and <= 'z' or >= '0' and <= '9';

    public static bool IsWhitespace(int? c) =>
        c is '\t' or ' ' or '\r';
}