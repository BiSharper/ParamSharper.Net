using ParamSharper.Flags;
using ParamSharper.Structure.Declaration;

namespace ParamSharper.Structure.Base;

public abstract class ParamContext : IParamDeclaration
{
    public virtual ParamAccessibility Accessibility => ParamAccessibility.Default;
    public bool NeedsProcessing { get; }
    public string ElementPath { get; }
    public virtual ParamContext? DeclarationOwner { get; }
    public string DeclarationName { get; }
    private readonly List<IParamStatement> _statements;
    private readonly Dictionary<string, IParamVariableDeclaration> _parameterCache = new();
    private readonly Dictionary<string, ParamContext> _contextCache = new();
    public IParamStatement this[int index] => _statements[index];

    protected ParamContext(string name, ParamContext? owner, List<IParamStatement> statements)
    {
        ElementPath = (DeclarationOwner = owner) is not null
            ? $"{DeclarationOwner.ElementPath}.{DeclarationName = name}"
            : DeclarationName = name;
        _statements = statements;
        foreach (var paramStatement in _statements) switch (paramStatement)
        {
            case ParamContext context: _contextCache[context.DeclarationName] = context; break;
            case IParamVariableDeclaration variable: _parameterCache[variable.DeclarationName] = variable; break;
            case IParamDirective when !NeedsProcessing: NeedsProcessing = true; break;
        }
    }

    protected ParamContext(string name, ParamContext owner, IEnumerable<IParamStatement> statements) : this(name, owner, statements.ToList())
    {
    }

    public int StatementPosition(IParamStatement statement) => _statements.IndexOf(statement);

    public bool HasStatement(IParamStatement statement) => _statements.Contains(statement);

    public bool HasClass(ParamContext context) => _contextCache.ContainsValue(context);

    public bool HasClass(string name) => _contextCache.ContainsKey(name);

    public bool HasVariable(ParamVariableDeclaration variable) => _parameterCache.ContainsValue(variable);

    public bool HasVariable(string name) => _parameterCache.ContainsKey(name);

    public bool TryDeleteContext(string contextName)
    {
        throw new NotImplementedException(); //TODO Accessibility

    }

    public bool TryDeleteContext(ParamContext context)
    {
        throw new NotImplementedException(); //TODO Accessibility

    }

}