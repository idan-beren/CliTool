using static CliTool.Utils.GlobalVariables;

namespace CliTool.Actions.CustomActions;

public class PrintVariableAction : BaseAction
{
    public string? Name { get; set; }

    public override Task<bool> Act()
    {
        Console.WriteLine(GetVariableValue(Name!));
        return Task.FromResult(true);
    }
}