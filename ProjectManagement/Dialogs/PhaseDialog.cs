using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using ZestClientApi.Repository;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class PhaseDialog : IDialog<object>
    {
        public string ProjectCode { get; set; }

        public async Task StartAsync(IDialogContext context)
        {
            context.PostAsync("Enter the project code");
            context.Wait(this.GetProjectPhase);
        }

        public async Task GetProjectPhase(IDialogContext context, IAwaitable<object> result)
        {
            var projCode = await result as Activity;
            var activity = await result as Activity;
            ProjectCode = (projCode.Text);

            StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            var obj = JObject.Parse(userData.Data.ToString());
            var token = (string)obj["Token"];

            if (token == null)
            {
                await context.PostAsync("Need to Login to access data");
                context.Call(new UserLoginDialog(), ResumeAfteNullToken);

            }
            if (token != null)
            {
                PhaseClient pc = new PhaseClient();
                string phase = await pc.PhaseDetails(token, ProjectCode);

                await context.PostAsync($"Response is {phase}");
                context.Done(true);
            }
        }

        private async Task ResumeAfteNullToken(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Login Successful!!!");
            context.Done(true);
        }
    

        //public async Task StartAsync(IDialogContext context)
        //{
        //    var projectFormFlow = FormDialog.FromForm(ProjectSelectionForm.ProjectForm, FormOptions.PromptInStart);
        //    context.Call(projectFormFlow, ResumeAfterForm);
        //}

        //public async Task ResumeAfterForm(IDialogContext context, IAwaitable<ProjectSelectionForm> result)
        //{
        //    var message = await result;

        //    if (message.projectTypes.ToString().Equals("CryptoCurrency"))
        //    {

        //        //await context.PostAsync("Your project is in Testing Phase.");
        //        //context.Done(true);
        //    }
        //    else if (message.projectTypes.ToString().Equals("FaceDetection"))
        //    {
        //        await context.PostAsync("Your project is in Development Phase.");
        //        context.Done(true);
        //    }
        //    else if (message.projectTypes.ToString().Equals("ProjectManagement"))
        //    {
        //        await context.PostAsync("Your project is in REQM Phase.");
        //        context.Done(true);
        //    }
        //    else
        //    {
        //        await context.PostAsync("Your project is in Maintenance Phase.");
        //        context.Done(true);
        //    }
        //}
    }
}