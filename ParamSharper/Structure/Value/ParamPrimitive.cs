using System.Runtime.InteropServices;
using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Value;


[StructLayout(LayoutKind.Explicit)]
public readonly struct ParamPrimitive : IParamValue
{
    [field: FieldOffset(0)] public ParamVariableType Type { get; }
    [field: FieldOffset(sizeof(ParamVariableType))] private float FloatValue { get; }
    [field: FieldOffset(sizeof(ParamVariableType))] private float DoubleValue { get; }
    [field: FieldOffset(sizeof(ParamVariableType))] private string StringValue { get; }
    [field: FieldOffset(sizeof(ParamVariableType))] private string IntValue { get; }
    [field: FieldOffset(sizeof(ParamVariableType))] private ParamPrimitive[] ArrayValue { get; }
}



