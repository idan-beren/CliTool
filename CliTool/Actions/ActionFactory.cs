using CliTool.YamlHandling;

namespace CliTool.Actions;

public static class ActionFactory
{
    public static IAction Create(string name, string type, Dictionary<object, object> config)
    {
        return type switch
        {
            "Log" => new LogAction 
                { Name = name, Type = type, Configuration = Reflection.DictionaryToConfig<LogActionConfig>(config) },
            _ => throw new Exception($"Unknown action type: {type}")
        };
    }
}