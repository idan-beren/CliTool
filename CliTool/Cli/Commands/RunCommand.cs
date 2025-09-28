using System.CommandLine;
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
        
        this.SetHandler((file, dry, verbose) =>
        {
            GlobalVariables.SetVariable("LoggerLevel", verbose ? LogLevel.Debug : LogLevel.Information);

            var actions = fileOption.Apply(file);
            Console.WriteLine(actions);
            
            if (dry)
                dryOption.Apply(actions);
            if (verbose)
                verboseOption.Apply(actions);
        }, fileOption, dryOption, verboseOption);
    }
}