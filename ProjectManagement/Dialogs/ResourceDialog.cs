using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    public class ResourceDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Oopss!! No resources are available.");
            context.Done(true);
        }
    }
}