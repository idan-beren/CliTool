using CliTool.Utils;
using CliTool.YamlHandling;

namespace CliTool.Actions;

public record ImportActionConfig
{
    public string? Path { get; set; }
}

public class ImportAction : BaseAction<ImportActionConfig>
{
    public override void Act()
    {
        var actions = GetActions();
        foreach (var action in actions)
            action.Act();
    }

    private List<IAction> GetActions()
    {
        var yaml = File.ReadAllText(Configuration.Path!);
        var deserializedYaml = YamlDeserializer.Deserialize(yaml);
        List<IAction> actions = Extractor.ExtractActions(deserializedYaml);
        return actions;
    }
}