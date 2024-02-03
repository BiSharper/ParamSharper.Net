using System.Runtime.InteropServices;
using ParamSharper.Structure.Value;

namespace ParamSharper.Structure.Base;

public static class IParamValueExtensions
{
    public static ParamPrimitive AsPrimitive(this IParamValue value)
    {
        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
        try
        {
            return Marshal.PtrToStructure<ParamPrimitive>(ptr);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }
}