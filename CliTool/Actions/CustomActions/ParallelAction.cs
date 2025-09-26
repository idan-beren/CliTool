namespace CliTool.Actions.CustomActions;

public class ParallelAction : BaseAction
{
    public List<BaseAction>? Actions { get; set; }
    
    public override async Task<bool> Act()
    {
        var tasks = Actions!.Select(a => a.Act()).ToList();
        var results = await Task.WhenAll(tasks);
        
        return results.All(r => r);
    }
}