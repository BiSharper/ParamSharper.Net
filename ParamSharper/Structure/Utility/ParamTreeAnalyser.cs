using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Utility;

public static class ParamTreeAnalyser
{
    public static IEnumerable<IParamStatement> GatherPreviousDeclarations(IParamStatement statement, ParamContext context, bool recursive = false)
    {
        for (;;)
        {
            var position = context.StatementPosition(statement);

            if (position < 0)
                throw new InvalidOperationException($"{statement.ElementPath} is not a child of {context.ElementPath}!");

            for (var i = 0; i < position; i++) yield return context[i];

            if (recursive && context.DeclarationOwner is { } owner)
            {
                statement = context;
                context = owner;
                recursive = false;
                continue;
            }

            break;
        }
    }
}