using System;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    //[LuisModel("75a5d7cb-a133-4c5b-9f1e-1629d7c9acb9", "1d85b742f01843d086962485f0728cd5")]
    [Serializable]
    public class InformationDialog : IDialog<object>
    {
        
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Finding...");

            context.Wait(this.GetInfo);
            //throw new NotImplementedException();
        }

        public async Task GetInfo(IDialogContext context, IAwaitable<object> result)
        {
            //var msg = await result;
            await context.PostAsync("Sorry, No information available.");
            context.Done(true);
        }

    }
}