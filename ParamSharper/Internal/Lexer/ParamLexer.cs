using System.Runtime.CompilerServices;
using System.Text;

namespace ParamSharper.Internal.Lexer;

internal ref struct ParamLexer
{
    private readonly TextReader _reader;
    private readonly StringBuilder _tokenBuilder = new();
    private int _charCache = -1;
    public int Line { get; private set; }
    public int Column { get; private set;}
    public ParamLexerMode Mode { get; private set; }

    public ParamLexer(
        TextReader reader,
        int startingLine = 0,
        int startingColumn = 0
    )
    {
        _reader = reader;
        Line = startingLine;
        Column = startingColumn;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public ParamToken Lex()
    {
        for (;;)
        {
            int c;
            if (_charCache == -1) c = _reader.Read();
            else { c = _charCache; _charCache = -1; }
            _tokenBuilder.Clear();
            _tokenBuilder.Append(c);
            switch (c)
            {
                case ' ' or '\t': continue;
                case '\r': continue;
                case '\n': Line++; Column = 0; continue;
                case '{': return GenerateToken(ParamLexeme.LeftCurly);
                case ',': return GenerateToken(ParamLexeme.Comma);
                case ':': return GenerateToken(ParamLexeme.Colon);
                case ';': return GenerateToken(ParamLexeme.Semicolon);

                default:
                    throw new NotImplementedException();
            }
        }

    }

    private ParamToken GenerateToken(ParamLexeme lexeme) => new()
    {
        Type = lexeme,
        Text = _tokenBuilder.ToString(),
        Column = Column,
        Line = Line
    };

    //No pattern needed as no finalizers exist
    public void Dispose() => _reader.Dispose();
}