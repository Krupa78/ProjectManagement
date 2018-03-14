using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    class ReOpenedTicketDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Here is the list of re-open tasks assigned to you.");
            await context.PostAsync("1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
            context.Done(true);
        }
    }
}