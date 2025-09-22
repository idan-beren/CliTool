namespace CliTool.Actions;

public interface IAction
{
    public string Name  { get; set; }
    public string Type   { get; set; }

    public abstract void Act();
}