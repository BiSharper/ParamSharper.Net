using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Directive;

public readonly struct ParamDeleteDirective : IParamDirective
{
    public string ElementPath { get; }
    public string Target { get; }

    public ParamDeleteDirective(string target, string elementPath)
    {
        Target = target;
        ElementPath = elementPath;
    }

    public ParamDeleteDirective(string target, ParamContext parent)
    {
        Target = target;
        ElementPath = $"{parent.ElementPath}.{Target}[delete]";
    }

    public bool TryCompute(ParamContext computationContext, out IParamDeclaration? created)
    {
        created = null;

        return computationContext.TryDeleteContext(Target);
    }

}