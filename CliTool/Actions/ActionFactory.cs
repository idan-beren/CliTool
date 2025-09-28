using CliTool.Actions.CustomActions;
using CliTool.Utils;

namespace CliTool.Actions;

public static class ActionFactory
{
    private static readonly Dictionary<string, Type> ActionTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Log", typeof(LogAction) },
        { "Parallel", typeof(ParallelAction) },
        { "Import", typeof(ImportAction) },
        { "Retry", typeof(RetryAction) },
        { "SetVariable", typeof(SetVariableAction) },
        { "PrintVariable", typeof(PrintVariableAction) },
        { "Assert", typeof(AssertAction) },
        { "Http", typeof(HttpAction) },
        { "Shell", typeof(ShellAction) },
        { "Condition", typeof(ConditionAction) },
        { "Delay", typeof(DelayAction) }
    };

    public static BaseAction Create(string actionType)
    {
        if (!ActionTypes.TryGetValue(actionType, out var type))
            throw new Exception($"Unknown action type: {actionType}");

        var instance = (BaseAction?)Activator.CreateInstance(type)
                       ?? throw new Exception($"Failed to create action of type {type.Name}");

        instance.Logger = new ActionLogger(category: $"{actionType}Action");
        return instance;
    }

    public static Type GetType(string actionType)
    {
        if (!ActionTypes.TryGetValue(actionType, out var type))
            throw new Exception($"Unknown action type: {actionType}");
        return type;
    }
}