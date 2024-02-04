using System.Runtime.InteropServices;
using ParamSharper.Structure.Value;

namespace ParamSharper.Structure.Base;

public static class IParamValueExtensions
{
    public static ParamPrimitive AsPrimitive(this IParamValue value) =>
        UnsafeValueCast<ParamPrimitive>(value);

    public static ParamValue<T> AsTyped<T>(this IParamValue value) where T : struct =>
        UnsafeValueCast<ParamValue<T>>(value);

    private static T UnsafeValueCast<T>(IParamValue value)
    {
        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
        try
        {
            return Marshal.PtrToStructure<T>(ptr)!;
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }
}