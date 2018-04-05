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
    class SprintDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("1. Project : Cryptocurrency \n" + "Deadline : 5 months \n" +
            //    "2. Project : Face Detection \n" + "Deadline : 5 months \n");

            context.Wait(SprintDetails);
            return Task.CompletedTask;
        }

        private async Task SprintDetails(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            var obj = JObject.Parse(userData.Data.ToString());
            var token = (string)obj["Token"];
            //ProductClient pc = new ProductClient();
            //string t = await pc.ProductDetails(token);

            //await context.PostAsync($"Response is {t}");
            //context.Done(true);
            if (token == null)
            {
                await context.PostAsync("Need to Login to access data");
                context.Call(new UserLoginDialog(), ResumeAfteNullToken);

            }
            if (token != null)
            {
                SprintClient proc = new SprintClient();
                string temp = await proc.SprintDetails(token);

                await context.PostAsync($"Response is {temp}");
                context.Done(true);
            }
        }

        private async Task ResumeAfteNullToken(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Login Successful!!!");
            context.Done(true);
        }
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
    //        var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
    //        context.Call(sprintFormFlow, ResumeAfterSprint);
    //    }
    //    else if(message.projectTypes.ToString().Equals("FaceDetection"))
    //    {
    //        var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
    //        context.Call(sprintFormFlow, ResumeAfterSprint);
    //    }
    //    else if (message.projectTypes.ToString().Equals("ProjectManagement"))
    //    {
    //        var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
    //        context.Call(sprintFormFlow, ResumeAfterSprint);
    //    }
    //    else
    //    {
    //        var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
    //        context.Call(sprintFormFlow, ResumeAfterSprint);
    //    }
    //}

    //public async Task ResumeAfterSprint(IDialogContext context, IAwaitable<object> result)
    //{
    //    var message = await result;
    //    await context.PostAsync("Sprint task:: \n" + "1. ICR-1 Develop login page. \n" + "2. ICR-2 Develop home page.");
    //    context.Done(true);
    //}

}
