using System.Diagnostics;
using System.Runtime.CompilerServices;
using ParamSharper.Structure.Base;

namespace ParamSharper.Structure.Utility;

public static class ParamStructureAnalyser
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IParamStatement> GatherPreviousStatements
    (
        IParamDeclaration declaration,
        bool recursive = false
    ) => GatherPreviousStatements(declaration, declaration.DeclarationOwner, recursive);

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static IEnumerable<IParamStatement> GatherPreviousStatements
    (
        IParamStatement statement,
        ParamContext statementOwner,
        bool recursive = false
    )
    {

        var position = statementOwner.StatementPosition(statement);

        if (position < 0)
            throw new InvalidOperationException($"{statement.ElementPath} is not a child of {statementOwner.ElementPath}!");

        for (;;)
        {
            for (var i = 0; i < position; i++) yield return statementOwner[i];

            if (recursive && statementOwner.DeclarationOwner is { } owner)
            {
                statement = statementOwner = owner;
                position = statementOwner.StatementPosition(statement);
                Debug.Assert(position > -1);
                recursive = false;
                continue;
            }

            break;
        }
    }
}