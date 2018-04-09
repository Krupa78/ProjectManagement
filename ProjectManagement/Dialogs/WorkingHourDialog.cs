using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class WorkingHourDialog : IDialog<object>
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
                var workingHourFormFlow = FormDialog.FromForm(CompletedRemainingHourForm.WorkingHourForm, FormOptions.PromptInStart);
                context.Call(workingHourFormFlow, ResumeAfterWorkingHourForm);
            }
            else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
            {
                var workingHourFormFlow = FormDialog.FromForm(CompletedRemainingHourForm.WorkingHourForm, FormOptions.PromptInStart);
                context.Call(workingHourFormFlow, ResumeAfterWorkingHourForm);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                var workingHourFormFlow = FormDialog.FromForm(CompletedRemainingHourForm.WorkingHourForm, FormOptions.PromptInStart);
                context.Call(workingHourFormFlow, ResumeAfterWorkingHourForm);
            }
            else
            {
                var workingHourFormFlow = FormDialog.FromForm(CompletedRemainingHourForm.WorkingHourForm, FormOptions.PromptInStart);
                context.Call(workingHourFormFlow, ResumeAfterWorkingHourForm);
            }
        }

        public async Task ResumeAfterWorkingHourForm(IDialogContext context, IAwaitable<CompletedRemainingHourForm> result)
        {
            var message = await result;
            if (message.hourTypes.ToString().Equals("CompletedHour"))
            {
                await context.PostAsync("Completed hours : 500 hrs");
                context.Done(true);
            }
            else if (message.hourTypes.ToString().Equals("RemainingHour"))
            {
                await context.PostAsync("Remaining hours : 400 hrs");
                context.Done(true);
            }
        }
    }
}