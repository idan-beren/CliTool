using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class RetryAction : BaseAction
{
    [Required(ErrorMessage = "Times is required")]
    public int Times { get; set; }
    
    [Required(ErrorMessage = "Action is required")]
    public BaseAction? Action { get; set; }
    
    public override async Task<bool> Act()
    {
        for (var i = 0; i < Times; i++)
        {
            Logger.LogDebug("Trying action");
            var result = await Action!.Act();
            Logger.LogDebug("Action returned {Result}", result);
            
            if (!result) continue;
            Logger.LogInformation("Action succeeded");
            return true;
        }
        Logger.LogInformation("Action failed");
        return false;
    }
}