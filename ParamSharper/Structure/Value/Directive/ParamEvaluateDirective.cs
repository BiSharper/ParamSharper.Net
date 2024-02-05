using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Value.Directive;

public readonly record struct ParamEvaluateDirective : IParamValueDirective
{
    public string ElementPath { get; }
    public bool Rapable => false;
    public ParamVariableType Type => ParamVariableType.Directive;
    public string Expression { get; }
    public ParamEvaluateDirective(string expression, string elementPrefix)
    {
        Expression = expression;
        ElementPath = $"{elementPrefix}.eval";
    }

    public ParamEvaluateDirective(string expression, ParamContext parent) : this(expression, parent.ElementPath)
    {
    }

    public bool TryCompute(ParamContext computationContext, out IParamValue? created)
    {
        throw new NotImplementedException();
    }

}