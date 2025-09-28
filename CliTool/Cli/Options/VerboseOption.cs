using System.CommandLine;
using CliTool.Actions;

namespace CliTool.Cli.Options;

public class VerboseOption() : Option<bool>(name: Name, description: Description)
{
    private new const string Name = "--verbose";
    private new const string Description = "Executes all actions - Shows additional information for each action.";
    
    public async Task<bool> Apply(List<BaseAction> actions)
    {
        foreach (var action in actions)
            await action.Act();
        return true;
    }
}