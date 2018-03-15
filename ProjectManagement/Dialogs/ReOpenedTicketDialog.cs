using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class ReOpenedTicketDialog : IDialog<object>
    {
            public async Task StartAsync(IDialogContext context)
            {
                var projectFormFlow = FormDialog.FromForm(ProjectSelectionForm.ProjectForm, FormOptions.PromptInStart);
                context.Call(projectFormFlow, ResumeAfterForm);
            }

        public async Task ResumeAfterForm(IDialogContext context, IAwaitable<ProjectSelectionForm> result)
        {
            var message = await result;

            if (message.projectTypes.ToString().Equals("CryptoCurrency"))
            {
                await context.PostAsync("Here is the list of re-open tasks assigned to you.");
                await context.PostAsync("1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                await context.PostAsync("Here is the list of re-open tasks assigned to you.");
                await context.PostAsync("1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("ProjectManagement"))
            {
                await context.PostAsync("Here is the list of re-open tasks assigned to you.");
                await context.PostAsync("1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
                context.Done(true);
            }
            else
            {
                await context.PostAsync("Here is the list of re-open tasks assigned to you.");
                await context.PostAsync("1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
                context.Done(true);
            }
            
        }

       
    }
}