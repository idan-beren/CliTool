using CliTool.Utils;

namespace CliTool.Actions;

public static class ActionFactory
{
    public static IAction Create(string name, string type, dynamic config)
    {
        return type switch
        {
            "Log" => new LogAction 
                { Name = name, Type = type, Configuration = Reflection.DictionaryToConfig<LogActionConfig>(config) },
            "Parallel" => new ParallelAction
                { Name = name, Type = type, Configuration = new ParallelActionConfig { Actions = Extractor.ExtractActions(config) } },
            _ => throw new Exception($"Unknown action type: {type}")
        };
    }
}