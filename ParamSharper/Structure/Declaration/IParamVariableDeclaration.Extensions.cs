using ParamSharper.Structure.Base;
using ParamSharper.Structure.Value;

namespace ParamSharper.Structure.Declaration;

public static class IParamVariableDeclarationExtensions
{
    public static ParamPrimitive AsPrimitive(this IParamVariableDeclaration declaration) =>
        declaration.GetValueBoxed().AsPrimitive();

    public static ParamPrimitive AsPrimitive<T>(this ParamVariableDeclaration<T> declaration) where T : IParamValue =>
        declaration.ValuePrimitive;

    public static ParamValue<T> AsTyped<T>(this IParamVariableDeclaration declaration) where T : struct =>
        declaration.GetValueBoxed().AsTyped<T>();
}