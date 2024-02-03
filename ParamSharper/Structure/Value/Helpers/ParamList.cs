using System.Collections;
using System.Runtime.InteropServices;
using ParamSharper.Flags;

namespace ParamSharper.Structure.Value.Helpers;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ParamList<T> : IParamValue, IEnumerable<T> where T : IParamValue
{
    public ParamVariableType Type => ParamVariableType.Array;
    [field: FieldOffset(0)] public List<T> Values { get; }

    public ParamList(List<T> values) => Values = values;

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}