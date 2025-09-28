using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class ConditionAction : AssertAction
{
    [Required(ErrorMessage = "Then is required")]
    public List<BaseAction>? Then { get; set; }
    
    [Required(ErrorMessage = "Else is required")]
    public List<BaseAction>? Else { get; set; }

    public override async Task<bool> Act()
    {
        var result = await Assert();
        Logger.LogDebug("Condition was asserted");
        
        var actionsToRun = result ? Then : Else;
        foreach (var action in actionsToRun!)
            await action.Act();
        
        Logger.LogInformation("Condition status: {Result}",  result);
        return result;
    }
}