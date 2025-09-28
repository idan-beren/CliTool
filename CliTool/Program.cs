using System.CommandLine;
using CliTool.Cli.Commands;

namespace CliTool;

internal static class Program
{
    private const string Description = "YAML-Based Action Runner CLI";
    
    static async Task<int> Main(string[] args)
    {
        await RunCommands(args);
        return Environment.ExitCode;
    }

    private static async Task RunCommands(string[] args)
    {
        var rootCommand = new RootCommand(Description);
        var runCommand = new RunCommand();
        rootCommand.AddCommand(runCommand);
        await rootCommand.InvokeAsync(args);
    }
}