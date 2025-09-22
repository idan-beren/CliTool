namespace CliTool.Actions;

public record LogActionConfig
{
    public string Message { get; set; }
}

public class LogAction : BaseAction<LogActionConfig>
{
    public override void Act()
    {
        Console.WriteLine(Configuration.Message);
    }
}