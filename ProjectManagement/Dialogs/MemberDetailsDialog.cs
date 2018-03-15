using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class MemberDetailsDialog : IDialog<object>
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
                await context.PostAsync("Team members in Cryptocurrency: \n" +
                    "1. Business Analyst : Vinit Shah \n" + "2. Developers : Mayank Patel");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
            {
                await context.PostAsync("Team members in Attendance And PayRoll: \n" +
                    "1. Business Analyst : Vinit Shah \n" + "2. Developers : Mayank Patel");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                await context.PostAsync("Team members in Face Detection: \n" +
                    "1. Business Analyst : Vinit Shah \n" + "2. Developers : Mayank Patel");
                context.Done(true);
            }
            else
            {
                await context.PostAsync("Team members in Project Management: \n" +
                    "1. Business Analyst : Vinit Shah \n" + "2. Developers : Mayank Patel");
                context.Done(true);
            }
        }
    }
}