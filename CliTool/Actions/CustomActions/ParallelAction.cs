using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class ParallelAction : BaseAction
{
    [Required(ErrorMessage = "Actions are required")]
    public List<BaseAction>? Actions { get; set; }
    
    public override async Task<bool> Act()
    {
        var tasks = Actions!.Select(a => a.Act()).ToList();
        Logger.LogDebug("All actions started");
        
        var results = await Task.WhenAll(tasks);
        Logger.LogDebug("All actions finished");
        
        var result = results.All(r => r);
        Logger.LogInformation("All actions execution status: {Result}", result);
        return result;
    }
}