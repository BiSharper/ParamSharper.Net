namespace ParamSharper.Structure.Base;

public interface IParamDeclaration : IParamStatement
{
    public ParamContext? DeclarationOwner { get; }
    public string DeclarationName { get; }
}

