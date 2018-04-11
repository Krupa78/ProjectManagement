using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using ZestClientApi.Repository;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class ActualStartDateDialog : IDialog<object>
    {
        public string ProjectCode { get; set; }

        public async Task StartAsync(IDialogContext context)
        {

            context.PostAsync("**Enter the project code**");
            context.Wait(this.GetActualStartDate);
        }

        public async Task GetActualStartDate(IDialogContext context, IAwaitable<object> result)
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

                if (token != null)
                {
                    ProjectDetailsClient tc = new ProjectDetailsClient();
                    var details = await tc.AllDetails(token, ProjectCode);

                    await context.PostAsync($"Actual start date for {details.ResponseJSON.ProjName} is **{details.ResponseJSON.StartDate.ToLongDateString()}**");
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
            context.Done(true);
        }

    }
}