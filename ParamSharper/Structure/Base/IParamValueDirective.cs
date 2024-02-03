using System.Diagnostics.CodeAnalysis;

namespace ParamSharper.Structure.Base;

public interface IParamValueDirective : IParamDirective, IParamValue
{
    public bool TryCompute(ParamContext computationContext, [MaybeNullWhen(false)] out IParamValue? created);
}