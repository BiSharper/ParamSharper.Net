using System.Runtime.InteropServices;
using ParamSharper.Flags;
using ParamSharper.Structure.Base;
using ParamSharper.Structure.Value;

namespace ParamSharper.Structure.Declaration;
public interface IParamVariableDeclaration : IParamDeclaration
{
    public ParamVariableType Type { get; }
    public ParamPrimitive ValuePrimitive { get; }
}

[StructLayout(LayoutKind.Explicit)]
public struct ParamVariableDeclaration : IParamVariableDeclaration
{
    [field: FieldOffset(0)] public string ElementPath { get; }
    [field: FieldOffset(sizeof(size_t))] public ParamContext DeclarationOwner { get; }
    [field: FieldOffset(sizeof(size_t) * 2)] public string DeclarationName { get; }

    // ReSharper disable once UnassignedGetOnlyAutoProperty
    [field: FieldOffset(sizeof(size_t) * 3)] public ParamVariableType Type { get; }
    [field: FieldOffset(sizeof(size_t) * 3)] public ParamPrimitive ValuePrimitive { get; }

    public ParamVariableDeclaration(string name, ParamPrimitive value, ParamContext owner)
    {
        ElementPath = $"{(DeclarationOwner = owner).ElementPath}.{DeclarationName = name}";
        ValuePrimitive = value;
    }

    public static implicit operator ParamPrimitive(ParamVariableDeclaration declaration) => declaration.ValuePrimitive;
}

[StructLayout(LayoutKind.Explicit)]
public struct ParamVariableDeclaration<T> : IParamVariableDeclaration
{
    [field: FieldOffset(0)] public string ElementPath { get; }
    [field: FieldOffset(sizeof(size_t))] public ParamContext DeclarationOwner { get; }
    [field: FieldOffset(sizeof(size_t) * 2)] public string DeclarationName { get; }
    // ReSharper disable three times UnassignedGetOnlyAutoProperty
    [field: FieldOffset(sizeof(size_t) * 2)] public ParamVariableType Type { get; }
    [field: FieldOffset(sizeof(size_t) * 2)] public ParamPrimitive ValuePrimitive { get; }
    [field: FieldOffset(sizeof(size_t) * 2)] public ParamValue<T> TypedValue { get; private set; }
    public T Value { get => TypedValue; set => TypedValue = TypedValue with { Value = value }; }

    public ParamVariableDeclaration(string name, ParamValue<T> value, ParamContext owner)
    {
        ElementPath = $"{(DeclarationOwner = owner).ElementPath}.{DeclarationName = name}";
        TypedValue = value;
    }

    public static implicit operator ParamValue<T>(ParamVariableDeclaration<T> declaration) => declaration.TypedValue;
}