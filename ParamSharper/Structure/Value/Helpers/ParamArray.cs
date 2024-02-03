using System.Collections;
using System.Runtime.InteropServices;
using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Value.Helpers;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ParamArray<T> : IParamValue, IEnumerable<T> where T : IParamValue
{
    [field: FieldOffset(0)] public ParamVariableType Type { get; } = ParamVariableType.Array;
    [field: FieldOffset(sizeof(ParamVariableType))] public T[] Values { get; }

    public ParamArray(T[] values) => Values = values;

    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
}