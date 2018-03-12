using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    public class TimeLineDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("1. Project : Cryptocurrency \n" + "Deadline : 5 months \n" +
                "1. Project : Cryptocurrency \n" + "Deadline : 5 months \n");
            context.Done(true);
        }
    }
}