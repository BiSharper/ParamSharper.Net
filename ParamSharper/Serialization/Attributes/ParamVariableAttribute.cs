using ParamSharper.Structure.Value;

namespace ParamSharper.Serialization.Attributes;


[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ParamVariableAttribute : Attribute
{
    public string? Name { get; }

    public ParamVariableAttribute(string? name = null) => Name = name;
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class ParamVariableAttribute<T> : ParamVariableAttribute;
