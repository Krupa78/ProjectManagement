using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    [LuisModel("75a5d7cb-a133-4c5b-9f1e-1629d7c9acb9", "1d85b742f01843d086962485f0728cd5")]
    public class GeneralDialog
    {
        /*public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("BOTTTT");
            context.Wait(CheckGreetings);
        }

        public async Task CheckGreetings(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;

            if (message.Equals("Hello"))
            {
                await context.PostAsync("Hello, How can I help you?");
            }
        }*/
    }
}