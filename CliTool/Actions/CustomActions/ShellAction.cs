using System.Diagnostics;

namespace CliTool.Actions.CustomActions;

public class ShellAction : BaseAction
{
    public string? Command { get; set; }

    public override async Task<bool> Act()
    {
        if (string.IsNullOrWhiteSpace(Command))
        {
            Console.WriteLine("ShellCommandAction: Command is empty.");
            return false;
        }

        try
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

            using var process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            Console.WriteLine($"ShellAction Output: {output.TrimEnd()}");

            if (!string.IsNullOrWhiteSpace(error))
                Console.WriteLine($"ShellAction Error: {error}");

            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ShellAction: Exception occurred - {ex.Message}");
            return false;
        }
    }
}