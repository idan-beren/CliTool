namespace CliTool.Actions.CustomActions;

public class LogAction : BaseAction
{
    public string? Message { get; set; }
    
    public override Task<bool> Act()
    {
        Console.WriteLine(Message);
        return Task.FromResult(true);
    }
}