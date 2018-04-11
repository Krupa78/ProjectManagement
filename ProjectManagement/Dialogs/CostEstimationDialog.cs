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
    public class CostEstimationDialog : IDialog<object>
    {
        public string ProjectCode { get; set; }

        public async Task StartAsync(IDialogContext context)
        {

            context.PostAsync("**Enter the project code**");
            context.Wait(this.GetCost);
        }

        public async Task GetCost(IDialogContext context, IAwaitable<object> result)
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

                    await context.PostAsync($"Cost of {details.ResponseJSON.ProjName} is **{details.ResponseJSON.ProjCost}**");
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
        //        await context.PostAsync("Total cost for these project is 1 lakh");
        //        context.Done(true);
        //    }
        //    else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
        //    {
        //        await context.PostAsync("Total cost for these project is 2 lakh");
        //        context.Done(true);
        //    }
        //    else if (message.projectTypes.ToString().Equals("FaceDetection"))
        //    {
        //        await context.PostAsync("Total cost for these project is 3 lakh");
        //        context.Done(true);
        //    }
        //    else if (message.projectTypes.ToString().Equals("ProjectManagement"))
        //    {
        //        await context.PostAsync("Total cost for these project is 4 lakh");
        //        context.Done(true);
        //    }

        //}
    }
}

