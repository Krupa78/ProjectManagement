using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class CompletedProjectListDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Completed Project List : \n" 
                + "1. Cryptocurrency <br>"
                + "2. Face detection <br>"
                + "3. Attendance and Payroll <br>");
                context.Done(true);
            }
        }
    }