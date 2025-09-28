using System.ComponentModel.DataAnnotations;
using CliTool.Utils;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class ImportAction : BaseAction
{
    [Required(ErrorMessage = "Path is required")]
    public string? Path { get; set; }
    
    public override async Task<bool> Act()
    {
        var yaml = await File.ReadAllTextAsync(Path!);
        Logger.LogDebug("The file in {Path} was loaded", Path);
        
        var actions = ActionDeserializer.Deserialize(yaml);
        Logger.LogDebug("The actions were deserialized");

        foreach (var action in actions)
            await action.Act();
        
        Logger.LogInformation("All actions run");
        return true;
    }
}