namespace ParamSharper.Internal.Parser;

public ref struct ParamParser
{
    private readonly TextReader _reader;

    public ParamParser(TextReader reader)
    {
        _reader = reader;
    }
}