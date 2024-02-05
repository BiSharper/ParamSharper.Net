using System.Collections;
using System.Runtime.InteropServices;
using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Value;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ParamValue<T> : IParamValue where T : struct
{
    [field: FieldOffset(0)] public ParamVariableType Type { get; } = ParamVariableType.Custom;
    [field: FieldOffset(sizeof(ParamVariableType))] public T Value { get; init; }

    public ParamValue(T value)
    {
        Type = (Value = value) switch
        {
            float => ParamVariableType.Float,
            int => ParamVariableType.Int,
            double => ParamVariableType.Double,
            IEnumerable => ParamVariableType.Array,
            _ => ParamVariableType.Custom
        };
    }

    public static implicit operator T(ParamValue<T> value) => value.Value;
}