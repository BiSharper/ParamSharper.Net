using System.Diagnostics.CodeAnalysis;

namespace ParamSharper.Structure.Base;

public interface IParamDirective : IParamStatement
{
    public bool Rapable { get; }
}