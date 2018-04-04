using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class SprintEndDateDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
            context.Call(sprintFormFlow, ResumeAfterForm);
        }

        public async Task ResumeAfterForm(IDialogContext context, IAwaitable<SprintDetailForm> result)
        {
            var message = await result;
            if (message.sprintTypes.ToString().Equals("SprintA"))
            {
                await context.PostAsync("For Sprint A : \n" + "1. Planned Start Date : 01/01/2018 \n"
                    + "2. Planned End Date : 01/04/2018 \n");
                context.Done(true);
            }
            else if (message.sprintTypes.ToString().Equals("SprintB"))
            {
                await context.PostAsync("For Sprint B : \n" + "1. Planned Start Date : 01/01/2018 \n"
                    + "2. Planned End Date : 01/04/2018 \n");
                context.Done(true);

            }
            else if (message.sprintTypes.ToString().Equals("SprintC"))
            {
                await context.PostAsync("For Sprint C : \n" + "1. Planned Start Date : 01/01/2018 \n"
                    + "2. Planned End Date : 01/04/2018 \n");
                context.Done(true);
            }
            else if (message.sprintTypes.ToString().Equals("SprintD"))
            {
                await context.PostAsync("For Sprint D : \n" + "1. Planned Start Date : 01/01/2018 \n"
                    + "2. Planned End Date : 01/04/2018 \n");
                context.Done(true);
            }
        }
    }
}