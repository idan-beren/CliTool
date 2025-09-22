namespace CliTool.Actions;

public abstract class BaseAction<TConfig>
{
    public string Name  { get; set; }
    public string Type   { get; set; }
    public TConfig Configuration { get; set; }

    public abstract void Act();
}