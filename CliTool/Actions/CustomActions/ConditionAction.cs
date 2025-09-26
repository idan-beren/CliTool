namespace CliTool.Actions.CustomActions;

public class ConditionAction : AssertAction
{
    public List<BaseAction>? Then { get; set; }
    public List<BaseAction>? Else { get; set; }

    public override async Task<bool> Act()
    {
        if (string.IsNullOrWhiteSpace(Condition))
        {
            Console.WriteLine("ConditionAction: No condition provided.");
            return false;
        }

        var result = await Assert();
        var actionsToRun = result ? Then : Else;
        if (actionsToRun != null)
            foreach (var action in actionsToRun)
                await action.Act();

        return result;
    }
}