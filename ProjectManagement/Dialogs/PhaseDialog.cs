using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class PhaseDialog : IDialog<object>
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
                await context.PostAsync("Your project is in Testing Phase.");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                await context.PostAsync("Your project is in Development Phase.");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("ProjectManagement"))
            {
                await context.PostAsync("Your project is in REQM Phase.");
                context.Done(true);
            }
            else
            {
                await context.PostAsync("Your project is in Maintenance Phase.");
                context.Done(true);
            }
        }
    }
}