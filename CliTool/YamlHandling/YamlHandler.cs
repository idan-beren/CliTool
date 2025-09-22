using CliTool.Actions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CliTool.YamlHandling;

public static class YamlHandler
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(NullNamingConvention.Instance)
        .Build();

    public static List<IAction> Deserialize(string yaml)
    {
        var actions = new List<IAction>();
        var root = Deserializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(yaml)!;

        foreach (var actionInfo in root["Actions"])
        {
            var action = ActionFactory.Create(actionInfo["Name"].ToString()!, actionInfo["Type"].ToString()!, (Dictionary<object, object>)actionInfo["Configuration"]);
            actions.Add(action);
        }
        return actions;
    }
}