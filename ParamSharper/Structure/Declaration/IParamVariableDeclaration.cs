using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Declaration;

public interface IParamVariableDeclaration : IParamDeclaration
{
    public ParamVariableType Type { get; }

    public IParamValue GetValueBoxed();
}