using ParamSharper.Flags;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Directive;

public readonly struct ParamExecuteDirective : IParamDirective
{
    public string ElementPath { get; }
    public bool Rapable => false;
    public string Expression { get; }

    public ParamExecuteDirective(string expression, string elementPrefix)
    {
        Expression = expression;
        ElementPath = $"{elementPrefix}.exec";
    }
}