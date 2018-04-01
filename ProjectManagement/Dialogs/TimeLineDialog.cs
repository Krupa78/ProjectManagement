using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using ZestClientApi;
using ZestClientApi.Repository;

namespace ProjectManagement.Dialogs
{
    public class DeadLineDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("1. Project : Cryptocurrency \n" + "Deadline : 5 months \n" +
                "2. Project : Face Detection \n" + "Deadline : 5 months \n");

            PhaseOfProject p = new PhaseOfProject();

            string v= p.GetData();
            await context.PostAsync(v);

            context.Done(true);
        }
    }
}