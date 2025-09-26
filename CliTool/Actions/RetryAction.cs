namespace CliTool.Actions;

public record RetryActionConfig
{
    public int Times { get; set; }
    public List<IAction> Actions { get; set; }
}

public class RetryAction : BaseAction<RetryActionConfig>
{
    public override void Act()
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (var action in Configuration.Actions)
            {
                action.Act();
            }
        }
    }
}