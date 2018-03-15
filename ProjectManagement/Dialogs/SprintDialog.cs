using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class SprintDialog : IDialog<object>
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
                var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
                context.Call(sprintFormFlow, ResumeAfterSprint);
            }
            else if(message.projectTypes.ToString().Equals("FaceDetection"))
            {
                var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
                context.Call(sprintFormFlow, ResumeAfterSprint);
            }
            else if (message.projectTypes.ToString().Equals("ProjectManagement"))
            {
                var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
                context.Call(sprintFormFlow, ResumeAfterSprint);
            }
            else
            {
                var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
                context.Call(sprintFormFlow, ResumeAfterSprint);
            }
        }

        public async Task ResumeAfterSprint(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync("Sprint task:: \n" + "1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
            context.Done(true);
        }

    }
}