namespace CliTool.Actions;

public record ParallelActionConfig
{
    public List<IAction> Actions { get; set; }
}

public class ParallelAction : BaseAction<ParallelActionConfig>
{
    public override void Act()
    {
        var tasks = Configuration.Actions.Select(action => Task.Run(action.Act)).ToList();
        Task.WhenAll(tasks);
    }
}