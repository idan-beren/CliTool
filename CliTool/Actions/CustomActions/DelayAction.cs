using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions
{
    public class DelayAction : BaseAction
    {
        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        public override async Task<bool> Act()
        {
            await Task.Delay(Duration);
            Logger.LogInformation("Delayed {Duration} milli-seconds", Duration);
            return true;
        }
    }
}