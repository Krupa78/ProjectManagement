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
            context.PostAsync("**Enter the project code**");
            context.Wait(this.GetProjectPhase);
        }

        public async Task GetProjectPhase(IDialogContext context, IAwaitable<object> result)
        {
            var projCode = await result as Activity;
            var activity = await result as Activity;
            ProjectCode = (projCode.Text);

            StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            if (userData != null && userData.Data != null)
            {
                var obj = JObject.Parse(userData.Data.ToString());
                var token = (string)obj["Authorization_Token_ProjectManagement"];

                if(token !=null)
                {
                    ProjectDetailsClient pc = new ProjectDetailsClient();
                    var details = await pc.AllDetails(token, ProjectCode);

                    await context.PostAsync($"{details.ResponseJSON.ProjName} is in {details.ResponseJSON.ProjPhase}");
                    context.Done(true);
                }
                else
                {
                    await context.PostAsync("Need to Login to access data");
                    context.Call(new UserLoginDialog(), ResumeAfteNullToken);
                }
            }
            else
            {
                await context.PostAsync("Please Type **'Hello'** to Login ");
            }
        }

        private async Task ResumeAfteNullToken(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Login Successful!!!");
            //context.Done(true);
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