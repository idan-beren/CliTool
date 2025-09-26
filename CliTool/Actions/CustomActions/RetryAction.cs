namespace CliTool.Actions.CustomActions;

public class RetryAction : BaseAction
{
    public int Times { get; set; }
    public BaseAction? Action { get; set; }
    
    public override async Task<bool> Act()
    {
        for (int i = 0; i < Times; i++)
        {
            var result = await Action!.Act();
            if (result)
                return true;
        }
        return false;
    }
}