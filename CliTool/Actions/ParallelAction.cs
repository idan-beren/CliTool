namespace CliTool.Actions;

public record ParallelActionConfig
{
    public List<IAction> Actions { get; set; }
}

public class ParallelAction : BaseAction<ParallelActionConfig>
{
    public override async void Act()
    {
        var tasks = Configuration.Actions.Select(action => Task.Run(action.Act)).ToList();
        await Task.WhenAll(tasks);
    }
}