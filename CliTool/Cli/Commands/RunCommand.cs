using System.CommandLine;
using CliTool.Actions;
using CliTool.Cli.Options;
using CliTool.Utils;
using Microsoft.Extensions.Logging;

namespace CliTool.Cli.Commands;

public class RunCommand : Command
{
    private new const string Name = "run";
    private new const string Description = "Runs all of the actions in the given file.";

    public RunCommand() : base(name: Name, description: Description)
    {
        var fileOption = new FileOption();
        var dryOption = new DryOption();
        var verboseOption = new VerboseOption();
        
        AddOption(fileOption);
        AddOption(dryOption);
        AddOption(verboseOption);
        
        this.SetHandler(async (file, dry, verbose) =>
        {
            try
            {
                GlobalVariables.SetVariable("LoggerLevel", verbose ? LogLevel.Debug : LogLevel.Information);
                var actions = fileOption.Apply(file);
                if (dry)
                    dryOption.Apply(actions);
                if (verbose)
                    await verboseOption.Apply(actions);
                if (!dry && !verbose)
                    await Apply(actions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.ExitCode = 1;
            }
            
        }, fileOption, dryOption, verboseOption);
    }

    private static async Task Apply(List<BaseAction> actions)
    {
        foreach (var action in actions)
            await action.Act();
    }
}