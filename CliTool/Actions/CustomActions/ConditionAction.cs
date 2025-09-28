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
        var conditionResult = await Assert();
        Logger.LogDebug("Condition was asserted");
        
        var actions = conditionResult ? Then : Else;
        
        var actionsResult = true;
        foreach (var action in actions!)
            if (!await action.Act())
                actionsResult = false;
        
        var finalResult = actionsResult && conditionResult;
        Logger.LogInformation("Condition status: {Result}",  finalResult);
        return finalResult;
    }
}