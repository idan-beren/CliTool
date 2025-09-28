using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class ShellAction : BaseAction
{
    [Required(ErrorMessage = "Command is required.")]
    public string? Command { get; set; }

    public override async Task<bool> Act()
    {
        try
        {
            var process = await ProcessShellCommand();
            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Shell exception: {Message}", ex.Message);
            return false;
        }
    }

    private async Task<Process> ProcessShellCommand()
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = Environment.OSVersion.Platform == PlatformID.Win32NT ? "cmd.exe" : "/bin/bash",
            Arguments = Environment.OSVersion.Platform == PlatformID.Win32NT 
                ? $"/c {Command}" 
                : $"-c \"{Command}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        Logger.LogDebug("Process started");

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();
            
        Logger.LogInformation("Process output: {Output}", output.Trim());

        if (!string.IsNullOrWhiteSpace(error))
            Logger.LogInformation("Process error: {Error}", error.Trim());
        return process;
    }
}