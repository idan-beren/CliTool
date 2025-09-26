using System.Threading.Tasks;

namespace CliTool.Actions.CustomActions
{
    public class DelayAction : BaseAction
    {
        public int Duration { get; set; }

        public override async Task<bool> Act()
        {
            await Task.Delay(Duration);
            return true;
        }
    }
}