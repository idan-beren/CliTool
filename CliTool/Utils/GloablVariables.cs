namespace CliTool.Utils;

public static class GlobalVariables
{
    private static readonly Dictionary<string, object?> Variables =  new();

    public static bool SetVariable(string key, object value)
    {
        return Variables.TryAdd(key, value);
    }

    public static object? GetVariableValue(string varName)
    {
        return Variables.GetValueOrDefault(varName);
    }
    
    public static Dictionary<string, object?> GetAllVariables()
    {
        return Variables;
    }
}