using System.Runtime.InteropServices;

namespace ParamSharper.Structure.Value;

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