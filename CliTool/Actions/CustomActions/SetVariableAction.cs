using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using static CliTool.Utils.GlobalVariables;

namespace CliTool.Actions.CustomActions;

public class SetVariableAction : BaseAction
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Value is required")]
    public object? Value { get; set; }

    public override Task<bool> Act()
    {
        var result = Task.FromResult(SetVariable(Name!, Value!));
        Logger.LogInformation(result.Result ? "Set variable {Name} to {Value}" : "Not set variable {Name} to {Value}",
            Name, Value);
        return result;
    }
}