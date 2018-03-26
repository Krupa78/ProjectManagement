using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    class RemainingTaskDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Remaining Task for today:: \n" 
                + "1. ICR-1 Develop login page. \n" 
                + "2. ICR-2 Develop home page.");
            context.Done(true);
        }
    }
}