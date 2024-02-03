using ParamSharper.Structure.Base;

namespace ParamSharper.Structure;

public class ParamDocument : ParamContext
{
    public ParamDocument(string name, ParamContext? owner, List<IParamStatement> statements) : base(name, owner, statements)
    {
    }

    public ParamDocument(string name, ParamContext owner, IEnumerable<IParamStatement> statements) : base(name, owner, statements)
    {
    }
}