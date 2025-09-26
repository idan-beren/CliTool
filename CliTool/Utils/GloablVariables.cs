using System.Collections.Concurrent;

namespace CliTool.Utils;

public static class GlobalVariables
{
    private static readonly ConcurrentDictionary<string, object?> Variables =  new();

    public static bool SetVariable(string key, object value)
    {
        return Variables.TryAdd(key, value);
    }

    public static object? GetVariableValue(string varName)
    {
        return Variables.GetValueOrDefault(varName);
    }
    
    public static IReadOnlyDictionary<string, object?> GetAllVariables()
    {
        return Variables;
    }
}