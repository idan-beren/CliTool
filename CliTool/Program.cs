using System.CommandLine;
using CliTool.Cli.Commands;

namespace CliTool;

internal static class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("YAML-Based Action Runner CLI");
        
        var runCommand = new RunCommand();
        rootCommand.AddCommand(runCommand);
        
        return await rootCommand.InvokeAsync(args);
    }
}