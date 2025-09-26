using CliTool.Actions.CustomActions;

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

    public static Type Resolve(string actionType)
    {
        if (ActionTypes.TryGetValue(actionType, out var type))
            return type;
        throw new Exception($"Unknown action type: {actionType}");
    }
}