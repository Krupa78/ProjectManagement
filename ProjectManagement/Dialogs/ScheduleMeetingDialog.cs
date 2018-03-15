using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class ScheduleMeetingDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var meetingFormFlow = FormDialog.FromForm(ScheduleMeetingForm.MeetingForm, FormOptions.PromptInStart);
            context.Call(meetingFormFlow, ResumeAfterForm);
        }

        public async Task ResumeAfterForm(IDialogContext context, IAwaitable<ScheduleMeetingForm> result)
        {
            var message = await result;
            await context.PostAsync("your meeting has been scheduled.");
            await context.PostAsync(message.meetingWith.ToString());
            context.Done(true);
        }
    }
}