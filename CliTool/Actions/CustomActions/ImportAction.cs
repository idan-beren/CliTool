using CliTool.Utils;

namespace CliTool.Actions.CustomActions;

public class ImportAction : BaseAction
{
    public string? Path { get; set; }
    
    public override Task<bool> Act()
    {
        var yaml = File.ReadAllText(Path!);
        var actions = ActionDeserializer.Deserialize(yaml);
        foreach (var action in actions)
            action.Act();
        return Task.FromResult(true);
    }
}