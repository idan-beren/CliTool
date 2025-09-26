using System.Reflection;

namespace CliTool.Utils;

public static class Reflection
{
    public static TConfig DictionaryToConfig<TConfig>(Dictionary<object, object> dict) where TConfig : new()
    {
        var config = new TConfig();
        var props = typeof(TConfig).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
        {
            if (!dict.TryGetValue(prop.Name, out var value)) continue;
            try
            { prop.SetValue(config, Convert.ChangeType(value, prop.PropertyType)); }
            catch (Exception)
            { return config; }
        }

        return config;
    }
}