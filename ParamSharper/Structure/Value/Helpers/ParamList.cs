using System.Collections;
using System.Runtime.InteropServices;
using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Value.Helpers;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ParamList<T> : IParamValue, IEnumerable<T> where T : IParamValue
{
    [field: FieldOffset(0)] public ParamVariableType Type { get; } = ParamVariableType.Array;
    [field: FieldOffset(sizeof(ParamVariableType))]  public List<T> Values { get; }

    public ParamList(List<T> values) => Values = values;

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}