using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Declaration;

public interface IParamVariableDeclaration : IParamDeclaration
{
    public ParamVariableType Type { get; }

    ParamContext IParamDeclaration.DeclarationOwner { get; }

    public IParamValue GetValueBoxed();
}