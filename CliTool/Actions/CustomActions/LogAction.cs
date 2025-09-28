using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class LogAction : BaseAction
{
    [Required(ErrorMessage = "Message is required")]
    public string? Message { get; set; }
    
    public override Task<bool> Act()
    {
        Logger.LogInformation("The message is: {Message}", Message);
        return Task.FromResult(true);
    }
}