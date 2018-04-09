using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using ZestClientApi.Repository;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class TechnologyOfProjectDialog : IDialog<object>
    {
        public string ProjectCode { get; set; }

        public async Task StartAsync(IDialogContext context)
        {

            context.PostAsync("**Enter the project code**");
            context.Wait(this.GetProjectTechnology);
        }

        public async Task GetProjectTechnology(IDialogContext context, IAwaitable<object> result)
        {
            var projCode = await result as Activity;
            var activity = await result as Activity;
            ProjectCode = (projCode.Text);

            StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            var obj = JObject.Parse(userData.Data.ToString());
            var token = (string)obj["Token"];

            if (token != null)
            {
                TechnologyClient tc = new TechnologyClient();
                string technology = await tc.TechnologyDetails(token, ProjectCode);

                await context.PostAsync($"{technology} technologies are used for this project");
                context.Done(true);
            }
            else
            {
                await context.PostAsync("Need to Login to access data");
                context.Call(new UserLoginDialog(), ResumeAfteNullToken);
            }
        }
        private async Task ResumeAfteNullToken(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Login Successful!!!");
            context.Done(true);
        }

    }
}