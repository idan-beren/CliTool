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
        var var = GetVariableValue(Name!);
        if (var == null)
        {
            Logger.LogInformation("The variable doesn't exist");
            return Task.FromResult(false);
        }
        Logger.LogInformation("The value of the variable {Name} is {Variable}", Name!, var);
        return Task.FromResult(true);
    }
}