using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class ProjectListDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var projectListFormFlow = FormDialog.FromForm(ProjectListForm.ProjectForm, FormOptions.PromptInStart);
            context.Call(projectListFormFlow, ResumeAfterForm);
        }

        public async Task ResumeAfterForm(IDialogContext context, IAwaitable<ProjectListForm> result)
        {
            var message = await result;
            if (message.listTypes.ToString().Equals("CurrentProjects"))
            {
                await context.PostAsync("Current Project List : \n" + "1. Cryptocurrency");
                context.Done(true);
            }
            else if (message.listTypes.ToString().Equals("CompletedProjects"))
            {
                await context.PostAsync("Completed Project List : \n" + "1. Cryptocurrency");
                context.Done(true);
            }
            else if (message.listTypes.ToString().Equals("UnderMaintenance"))
            {
                await context.PostAsync("Under Maintenace Project List : \n" + "1. Cryptocurrency");
                context.Done(true);
            }
        }
    }
}