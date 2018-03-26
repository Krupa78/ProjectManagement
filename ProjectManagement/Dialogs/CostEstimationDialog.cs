using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class CostEstimationDialog : IDialog<object>
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
                await context.PostAsync("Total cost for these project is 1 lakh");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
            {
                await context.PostAsync("Total cost for these project is 2 lakh");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                await context.PostAsync("Total cost for these project is 3 lakh");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("ProjectManagement"))
            {
                await context.PostAsync("Total cost for these project is 4 lakh");
                context.Done(true);
            }
           
        }
    }
}

