using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using ZestClientApi;
using ZestClientApi.Repository;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class DeadLineDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("1. Project : Cryptocurrency \n" + "Deadline : 5 months \n" +
                "2. Project : Face Detection \n" + "Deadline : 5 months \n");
            context.Done(true);
            //context.Wait(DeadLineReport);
            //return Task.CompletedTask;
        }

        //private async Task DeadLineReport(IDialogContext context, IAwaitable<object> result)
        //{
        //    var activity = await result as Activity;

        //    StateClient stateClient = activity.GetStateClient();
        //    BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
        //    var obj = JObject.Parse(userData.Data.ToString());
        //    var token = (string)obj["Token"];
        //    //ProductClient pc = new ProductClient();
        //    //string t = await pc.ProductDetails(token);

        //    //await context.PostAsync($"Response is {t}");
        //    //context.Done(true);
        //    if (token == null)
        //    {
        //        await context.PostAsync("Need to Login to access data");
        //        context.Call(new UserLoginDialog(), ResumeAfteNullToken);

        //    }
        //    if (token != null)
        //    {
        //        ProductClient proc = new ProductClient();
        //        string temp = await proc.ProductDetails(token);

        //        await context.PostAsync($"Response is {temp}");
        //        context.Done(true);
        //    }
        //}

        //private async Task ResumeAfteNullToken(IDialogContext context, IAwaitable<object> result)
        //{
        //    await context.PostAsync("Login Successful!!!");
        //}
    }
}