namespace CliTool.Actions;

public interface IAction
{
    public string? Type   { get; set; }
    
    public Task<bool> Act();
    
    public string ToString();
}