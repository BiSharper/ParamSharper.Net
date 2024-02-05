namespace BiSharper.LanguageUtils;

public interface IBisToken
{
    public string Text { get; }
    public int Line { get; }
    public int Column { get; }
}

public interface IBisToken<out TLexemes> : IBisToken where TLexemes : Enum
{
    public TLexemes Type { get; }
}