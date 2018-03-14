using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class WorkingHourDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("Here is the list of tasks assigned to you.");
            await context.PostAsync("Total hours : 100 hrs");
            context.Done(true);
        }
    }
}