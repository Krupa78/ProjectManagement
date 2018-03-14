using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    class ReleaseDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("Here is the list of tasks assigned to you.");
            await context.PostAsync("1. Planned Start Date : 01/01/2018 \n" 
                + "2. Planned End Date : 01/04/2018 \n");
            context.Done(true);
        }
    }
}