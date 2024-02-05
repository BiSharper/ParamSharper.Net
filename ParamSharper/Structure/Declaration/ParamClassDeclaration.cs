using ParamSharper.Flags;
using ParamSharper.Serialization.Attributes;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Declaration;

public sealed class ParamClassDeclaration : ParamContext
{

    [ParamVariable<ParamAccessibility>] public override ParamAccessibility Accessibility { get; }
    public override ParamContext? DeclarationOwner { get; } = null!;

    public ParamClassDeclaration(string name, ParamContext owner, List<IParamStatement> statements) : base(name, owner, statements)
    {
    }

    public ParamClassDeclaration(string name, ParamContext owner, IEnumerable<IParamStatement> statements) : base(name, owner, statements)
    {
    }
}