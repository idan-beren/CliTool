using System.CommandLine;
using CliTool.Actions;

namespace CliTool.Cli.Options;

public class DryOption() : Option<bool>(name: Name, description: Description)
{
    private new const string Name = "--dry";
    private new const string Description = "Prints all actions - Parses all the actions and then show them.";

    public void Apply(List<BaseAction> actions)
    {
        foreach (var action in actions)
            Console.WriteLine(action);
    }
}