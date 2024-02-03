using System.Diagnostics.CodeAnalysis;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Directive;

public interface IParamDirective : IParamStatement
{
    public bool TryCompute(ParamContext computationContext, [MaybeNullWhen(false)] out IParamDeclaration? created);
}