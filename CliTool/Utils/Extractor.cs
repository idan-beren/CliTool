using CliTool.Actions;

namespace CliTool.Utils;

public static class Extractor
{
    public static List<IAction> ExtractActions(dynamic root)
    {
        var actions = new List<IAction>();
        foreach (var actionInfo in root["Actions"])
        {
            var action = ActionFactory.Create(actionInfo["Name"].ToString()!, actionInfo["Type"].ToString()!, actionInfo["Configuration"]);
            actions.Add(action);
        }
        return actions;
    }
}