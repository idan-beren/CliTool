using CliTool.Utils;

namespace CliTool.Actions;

public record ImportActionConfig
{
    public string? Path { get; set; }
}

public class ImportAction : BaseAction<ImportActionConfig>
{
    public override void Act()
    {
        var actions = Extractor.ExtractActions(Configuration.Path!);
        foreach (var action in actions)
            action.Act();
    }
}