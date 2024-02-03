using System.Diagnostics.CodeAnalysis;

namespace ParamSharper.Structure.Base;

public interface IParamDeclarationDirective : IParamDirective
{
    public bool TryCompute(ParamContext computationContext, [MaybeNullWhen(false)] out IParamDeclaration? created);
}