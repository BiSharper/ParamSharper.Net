using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Declaration;

public sealed class ParamClassDeclaration : ParamContext
{
    public override ParamContext DeclarationOwner { get; } = null!;

    public ParamClassDeclaration(string name, ParamContext owner, List<IParamStatement> statements) : base(name, owner, statements)
    {
    }

    public ParamClassDeclaration(string name, ParamContext owner, IEnumerable<IParamStatement> statements) : base(name, owner, statements)
    {
    }
}