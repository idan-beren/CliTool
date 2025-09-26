using static CliTool.Utils.GlobalVariables;

namespace CliTool.Actions.CustomActions;

public class SetVariableAction : BaseAction
{
    public string? Name { get; set; }
    public object? Value { get; set; }

    public override Task<bool> Act()
    {
        return Task.FromResult(SetVariable(Name!, Value!));
    }
}