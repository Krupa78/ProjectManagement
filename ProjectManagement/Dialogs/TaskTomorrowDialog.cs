using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class TaskTomorrowDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var taskFormFlow = FormDialog.FromForm(CompletedRemainingTaskForm.TaskForm, FormOptions.PromptInStart);
            context.Call(taskFormFlow, ResumeAfterForm);
            //await context.PostAsync("Here is the list of tasks for today.");
            //await context.PostAsync("1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
            //context.Done(true);
        }

        public async Task ResumeAfterForm(IDialogContext context, IAwaitable<CompletedRemainingTaskForm> result)
        {
            var message = await result;

            if (message.taskTypes.ToString().Equals("RemainingTask"))
            {
                await context.PostAsync("Remaining Task for tomorrow:: \n" + "1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
                context.Done(true);
            }
            else
            {
                await context.PostAsync("Completed Task for tomorrow:: \n" + "1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
                context.Done(true);
            }
        }
    }
}