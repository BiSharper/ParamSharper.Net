using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Declaration.Directive;

public readonly record struct ParamDeleteDirective : IParamDeclarationDirective
{
    public bool Rapable => true;
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
        throw new NotImplementedException();
    }

    public bool TryCompute(ParamContext computationContext, out IParamElement? created)
    {
        created = null;

        return computationContext.TryDeleteContext(Target);
    }
}