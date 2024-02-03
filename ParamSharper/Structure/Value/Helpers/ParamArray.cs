using System.Collections;
using System.Runtime.InteropServices;
using ParamSharper.Flags;

namespace ParamSharper.Structure.Value.Helpers;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ParamArray<T> : IParamValue, IEnumerable<T> where T : IParamValue
{
    public ParamVariableType Type => ParamVariableType.Array;
    [field: FieldOffset(0)] public T[] Values { get; }

    public ParamArray(T[] values) => Values = values;

    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
}