using System.CommandLine;
using CliTool.Actions;

namespace CliTool.Cli.Options;

public class VerboseOption() : Option<bool>(name: Name, description: Description)
{
    private new const string Name = "--verbose";
    private new const string Description = "Executes all actions - Shows additional information for each action.";
    
    public async Task<bool> Apply(List<BaseAction> actions)
    {
        var result = true;
        foreach (var action in actions)
            if (!await action.Act())
                result = false;
        return result;
    }
}