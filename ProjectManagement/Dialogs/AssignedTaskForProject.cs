using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class AssignedTaskForProject : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Here is the list of tasks for today.");
            await context.PostAsync("1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
            //context.Done(true);
        }
    }

    
}