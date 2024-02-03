using System.Runtime.InteropServices;
using ParamSharper.Flags;

namespace ParamSharper.Structure.Value;

public interface IParamValue
{
    public ParamVariableType Type { get; }


}