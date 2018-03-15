using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    [LuisModel("75a5d7cb-a133-4c5b-9f1e-1629d7c9acb9", "1d85b742f01843d086962485f0728cd5")]
    public class GeneralDialog : LuisDialog<object>
    {

        [LuisIntent("GoodMorning")]
        public async Task GoodMorning(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Good Morning, Have a nice day!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greetings")]
        public async Task Greetings(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            var message = await activity as Activity;
         //   if (message.Text.Equals("Hello", StringComparison.InvariantCultureIgnoreCase))
           // {
                await context.PostAsync("Hello, Welcome to the Project Management BOT.");

                //call FormFlow to take designation details from user
                var designationFormFlow = FormDialog.FromForm(DesignationDetailsForm.DesignationForm, FormOptions.PromptInStart);
                context.Call(designationFormFlow, ResumeAfterDesignationForm);
            //}
            
        }

        public async Task ResumeAfterDesignationForm(IDialogContext context, IAwaitable<DesignationDetailsForm> result)
        {
            var message = await result;
            if (message.designationTypes.ToString().Equals("Developers"))
            {
                context.Call(new DevelopersDialog(), ResumeAfterAll);
            }   
        }

        public async Task ResumeAfterAll(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("How else can I help you?");
        }
    }
}