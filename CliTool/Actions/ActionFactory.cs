using CliTool.Utils;

namespace CliTool.Actions;

public static class ActionFactory
{
    public static IAction Create(string name, string type, Dictionary<object, object> config)
    {
        return type switch
        {
            "Log" => new LogAction
                { Name = name, Type = type, Configuration = Reflection.DictionaryToConfig<LogActionConfig>(config) },
            "Parallel" => new ParallelAction
                { Name = name, Type = type, Configuration = new ParallelActionConfig { Actions = Extractor.GetActions(config) } },
            "Import" => new ImportAction
                { Name = name, Type = type, Configuration = Reflection.DictionaryToConfig<ImportActionConfig>(config) },
            "Retry" => new RetryAction
                { Name = name, Type = type, Configuration = Reflection.DictionaryToConfig<RetryActionConfig>(config)
                    with { Actions = Extractor.GetActions(config) } },
            _ => throw new Exception($"Unknown action type: {type}")
        };
    }
}