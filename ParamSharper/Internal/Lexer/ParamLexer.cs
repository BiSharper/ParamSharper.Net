using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using ParamSharper.Internal.Lexer.Types;
using ParamSharper.Internal.Lexer.Utility;

namespace ParamSharper.Internal.Lexer;


internal ref struct ParamLexer
{
    private readonly TextReader _reader;
    private readonly StringBuilder _tokenBuilder = new();
    private int? _charCache = null;
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
            {
                if (_charCache is { } notNull)
                {
                    c = notNull; _charCache = null;
                } else c = _reader.Read();
            }


            switch (Mode)
            {
                case ParamLexerMode.Syntax:
                {
                    if(ParamTokenProof.IsWhitespace(c)) return ConsumeWhitespace(_charCache = c);
                    if(ParamTokenProof.IsIdentifierChar(_charCache = c))
                    {
                        if (ReadWordToken() is { } token) return token;
                        continue;
                    }
                    switch (c)
                    {
                        case '/': return ConsumeComment();
                        case '\r': continue; //Ignoring; CRLF is stupid
                        case '#': { if (ConsumeHash() is { } token) return token; continue; }
                        case '\n': return GenerateToken(ParamLexeme.EOL);
                        case '{': return GenerateToken(ParamLexeme.LeftCurly, c.ToString());
                        case '}': return GenerateToken(ParamLexeme.LeftCurly, c.ToString());
                        case ',': return GenerateToken(ParamLexeme.Comma, c.ToString());
                        case ':': return GenerateToken(ParamLexeme.Colon, c.ToString());
                        case ';': return GenerateToken(ParamLexeme.Semicolon, c.ToString());
                        case '[': return GenerateToken(ParamLexeme.RightSquare, c.ToString());
                        case ']': return GenerateToken(ParamLexeme.LeftSquare, c.ToString());
                        case '+': return ConsumeArrayOperator(ParamLexeme.SubAssign, ParamTokenProof.AddAssign);
                        case '-': return ConsumeArrayOperator(ParamLexeme.SubAssign, ParamTokenProof.SubAssign);
                        case '=': return ConsumeAssignOperator();
                        default: return GenerateToken(ParamLexeme.Invalid, c.ToString());
                    }
                }
                case ParamLexerMode.Value: 
                {
                    if(ParamTokenProof.IsWhitespace(c)) continue;
                    throw new NotImplementedException();

                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }

    private ParamToken ConsumeComment()
    {
        _tokenBuilder.Clear();
        _tokenBuilder.Append('/');
        _tokenBuilder.Append((char)(_charCache = _reader.Peek()));
        switch (_charCache)
        {
            case '/':
                do
                {
                    _tokenBuilder.Append((char)(_charCache = _reader.Read()));
                } while (_charCache != '\n');
                break;
            case '*':
                for (var previous = (int)_charCache; _charCache != '/' && previous != '/'; previous = (int)_charCache)
                {
                    _tokenBuilder.Append((char)_charCache);
                    _charCache = _reader.Read();
                }
                break;
            default: return GenerateToken(ParamLexeme.Invalid);
        }

        return GenerateToken(ParamLexeme.Preprocessor);
    }


    private ParamToken ConsumeArrayOperator(ParamLexeme assumedLexeme, string lexemeText)
    {
        if ((_charCache = _reader.Read()) != '=') return GenerateToken(ParamLexeme.Invalid, lexemeText);
        Mode = ParamLexerMode.Value;
        return GenerateToken(assumedLexeme, lexemeText);
    }

    private ParamToken ConsumeAssignOperator()
    {
        Mode = ParamLexerMode.Value;
        return GenerateToken(ParamLexeme.Assign, ParamTokenProof.Assign);
    }

    private ParamToken? ConsumeHash()
    {
        _tokenBuilder.Clear();
        _tokenBuilder.Append('#');
        _charCache = _reader.Peek();
        if (_charCache is '#')
        {
            for (_charCache = _reader.Peek(); ParamTokenProof.IsIdentifierChar(_charCache = _reader.Peek()); _reader.Read())
                _tokenBuilder.Append((char)_charCache);

            Mode = ParamLexerMode.PreProcessor;
            return null;
        }

        for (int previous = '#'; _charCache != '\n' || previous == '\\'; previous = (int)_charCache)
        {
            _tokenBuilder.Append((char)_charCache);
            _charCache = _reader.Read();
        }


        return GenerateToken(ParamLexeme.Preprocessor);
    }

    private ParamToken ConsumeWhitespace(int? firstChar)
    {
        Debug.Assert(ParamTokenProof.IsWhitespace(firstChar));
        _tokenBuilder.Clear();
        if(firstChar is not null || _charCache is not null) _tokenBuilder.Append((char)(firstChar ?? _charCache)!);
        bool isLineEnd;
        for
        (
            _charCache = _reader.Peek();
            ParamTokenProof.IsWhitespace(_charCache = _reader.Peek()) || !(isLineEnd = _charCache == '\n');
            _reader.Read()
        ) _tokenBuilder.Append((char)_charCache);

        if (!isLineEnd) return GenerateToken(ParamLexeme.Whitespace);

        IncrementLine();
        return GenerateToken(ParamLexeme.EOL);
    }

    private ParamToken? ReadWordToken(int? firstChar = null)
    {
        Debug.Assert(ParamTokenProof.IsIdentifierChar(firstChar));
        _tokenBuilder.Clear();
        _tokenBuilder.Append(firstChar ?? _charCache ?? throw new NotSupportedException("Cannot read word prematurely."));
        //TODO: Handle ## macro
        for (_charCache = _reader.Peek(); ParamTokenProof.IsIdentifierChar(_charCache = _reader.Peek()); _reader.Read())
        {
            _tokenBuilder.Append((char)_charCache);
        }

        var text = _tokenBuilder.ToString();
        switch (text)
        {
            case ParamTokenProof.KeywordExecute or ParamTokenProof.KeywordEvaluate:
                Mode = ParamLexerMode.PreProcessor;
                return null;
            case ParamTokenProof.KeywordDelete:
                return GenerateToken(ParamLexeme.Delete, ParamTokenProof.KeywordDelete);
            case ParamTokenProof.KeywordClass:
                return GenerateToken(ParamLexeme.Class, ParamTokenProof.KeywordClass);
            case ParamTokenProof.KeywordEnum:
                return GenerateToken(ParamLexeme.Enum, ParamTokenProof.KeywordEnum);
            default: return GenerateToken(ParamLexeme.Identifier, ParamTokenProof.KeywordEnum);
        }
    }



    private void IncrementLine()
    {
        Line++;
        Column = 0;
    }

    private ParamToken GenerateToken(ParamLexeme lexeme, string? text = null) => new()
    {
        Type = lexeme,
        Text = text ?? _tokenBuilder.ToString(),
        Column = Column,
        Line = Line
    };

    //No pattern needed as no finalizers exist
    public void Dispose() => _reader.Dispose();
}