using CliTool.Actions;
using CliTool.YamlHandling;

namespace CliTool.Utils;

public static class Extractor
{
    public static List<IAction> GetActions(dynamic root)
    {
        var actions = new List<IAction>();
        foreach (var actionInfo in root["Actions"])
        {
            var action = ActionFactory.Create(actionInfo["Name"].ToString()!, actionInfo["Type"].ToString()!, actionInfo["Configuration"]);
            actions.Add(action);
        }
        return actions;
    }

    public static List<IAction> ExtractActions(string path)
    {
        var yaml = File.ReadAllText(path);
        var deserializedYaml = YamlDeserializer.Deserialize(yaml);
        List<IAction> actions = GetActions(deserializedYaml);
        return actions;
    }
}