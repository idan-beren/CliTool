using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using static CliTool.Utils.GlobalVariables;

namespace CliTool.Actions.CustomActions;

public class PrintVariableAction : BaseAction
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public override Task<bool> Act()
    {
        Logger.LogInformation("The value of the variable {Name} is {Variable}", Name!, GetVariableValue(Name!));
        return Task.FromResult(true);
    }
}