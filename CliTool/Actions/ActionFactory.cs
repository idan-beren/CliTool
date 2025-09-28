using Microsoft.Extensions.Logging;
using CliTool.Actions.CustomActions;
using CliTool.Utils;

namespace CliTool.Actions
{
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

        public static ILogger CreateLogger(string actionType)
        {
            if (!ActionTypes.ContainsKey(actionType))
                throw new Exception($"Unknown action type: {actionType}");

            return new ActionLogger(category: $"{actionType}Action", (LogLevel)GlobalVariables.GetVariableValue("LoggerLevel")!);
        }

        public static Type GetType(string actionType)
        {
            if (!ActionTypes.TryGetValue(actionType, out var type))
                throw new Exception($"Unknown action type: {actionType}");
            return type;
        }
    }
}
