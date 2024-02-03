using ParamSharper.Flags;

namespace ParamSharper.Structure.Base;

public interface IParamValue : IParamElement
{
    public ParamVariableType Type { get; }
}