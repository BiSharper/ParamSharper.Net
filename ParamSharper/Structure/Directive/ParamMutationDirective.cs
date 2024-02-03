using ParamSharper.Structure.Base;
using ParamSharper.Structure.Value;
using ParamSharper.Structure.Value.Helpers;

namespace ParamSharper.Structure.Directive;

public readonly struct ParamMutationDirective : IParamDirective
{
    public string ElementPath { get; }
    public string Target { get; }
    public ParamArray<IParamValue> Mutation { get; }
    public bool Additive { get; }

    public ParamMutationDirective(string target, ParamArray<IParamValue> mutation, bool additive, string pathPrefix)
    {
        Target = target;
        Additive = additive;
        Mutation = mutation;
        ElementPath = $"{pathPrefix}.{Target}[{(Additive ? "additive" : "subtractive")}-mutation]";
    }

    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public ParamMutationDirective(string target, ParamArray<IParamValue> mutation, bool additive, ParamContext parent) : this(target, mutation, additive, parent.ElementPath)
    {
    }

    public bool TryCompute(ParamContext computationContext, out IParamDeclaration? created)
    {
        throw new NotImplementedException();
    }
}