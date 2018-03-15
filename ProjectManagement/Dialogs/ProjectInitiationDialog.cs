using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class ProjectInitiationDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var newProjectFormFlow = FormDialog.FromForm(ProjectCreationForm.ProjectForm, FormOptions.PromptInStart);
            context.Call(newProjectFormFlow, ResumeAfterForm);
        }

        public async Task ResumeAfterForm(IDialogContext context, IAwaitable<ProjectCreationForm> result)
        {
            await context.PostAsync("Project created...");
            context.Done(true);
        }
    }
}