using System.Runtime.InteropServices;
using ParamSharper.Flags;
using ParamSharper.Structure.Base;
using ParamSharper.Structure.Value;

namespace ParamSharper.Structure.Declaration;


[StructLayout(LayoutKind.Explicit)]
public record struct ParamVariableDeclaration : IParamVariableDeclaration
{
    [field: FieldOffset(0)] public string ElementPath { get; }
    [field: FieldOffset(sizeof(size_t))] public ParamContext DeclarationOwner { get; }
    [field: FieldOffset(sizeof(size_t) * 2)] public string DeclarationName { get; }
    // ReSharper disable once UnassignedGetOnlyAutoProperty
    [field: FieldOffset(sizeof(size_t) * 3)] public ParamVariableType Type { get; } //Assigned quietly via marshal
    [field: FieldOffset(sizeof(size_t) * 3)] private IParamValue _boxedValue;

    public ParamVariableDeclaration(string name, IParamValue value, ParamContext owner)
    {
        ElementPath = $"{(DeclarationOwner = owner).ElementPath}.{DeclarationName = name}";
        _boxedValue = value;
    }

    public IParamValue GetValueBoxed() => _boxedValue;

    public IParamValue SetValueBoxed(IParamValue value) => _boxedValue = value;

    public static implicit operator ParamPrimitive(ParamVariableDeclaration declaration) => declaration.AsPrimitive();
}

[StructLayout(LayoutKind.Explicit)]
public record struct ParamVariableDeclaration<T> : IParamVariableDeclaration where T: IParamValue
{
    [field: FieldOffset(0)] public string ElementPath { get; }
    [field: FieldOffset(sizeof(size_t))] public ParamContext DeclarationOwner { get; }
    [field: FieldOffset(sizeof(size_t) * 2)] public string DeclarationName { get; }
    // ReSharper disable three times UnassignedGetOnlyAutoProperty
    [field: FieldOffset(sizeof(size_t) * 3)] public ParamVariableType Type { get; }
    [field: FieldOffset(sizeof(size_t) * 3)] public ParamPrimitive ValuePrimitive { get; }
    [field: FieldOffset(sizeof(size_t) * 3)] public T Value { get; private set; }

    public ParamVariableDeclaration(string name, T value, ParamContext owner)
    {
        ElementPath = $"{(DeclarationOwner = owner).ElementPath}.{DeclarationName = name}";
        Value = value;
    }

    public IParamValue GetValueBoxed() => Value;

    public static implicit operator T(ParamVariableDeclaration<T> declaration) => declaration.Value;
}