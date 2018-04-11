using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;

using System.Threading.Tasks;
using ZestClientApi.Repository;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class UserLoginDialog : IDialog<object>
    {
        protected string username { get; set; }
        protected string password { get; set; }
        protected string AuthenticationType { get; set; }

        public Task StartAsync(IDialogContext context)
        {
            context.PostAsync("**Login with the chatbot to access the details regarding project management**");
            context.PostAsync("Please Enter LoginID:");

            context.Wait(GetUser);

            return Task.CompletedTask;

        }
        private async Task GetUser(IDialogContext context, IAwaitable<object> result)
        {
            var user = await result as Activity;
            username = (user.Text);

            await context.PostAsync("Enter password:");
            context.Wait(Authentication);
        }

        private async Task Authentication(IDialogContext context, IAwaitable<object> result)
        {
            var pass = await result as Activity;
            password = (pass.Text);

            var ac = new BotAuthenticationClient();
            string user = await ac.BotAuthentication(username, password);

            if (user != null)
            {
                var auth = new AuthenticationClient();
                string t = await auth.TokenCalling(username, password);
                if(t == null)
                {
                    await context.PostAsync("wrong credentials");
                    await context.PostAsync("Re-Enter LoginID ::");
                    context.Wait(GetUser);
                }
                if (t != null)
                {
                    await context.PostAsync($"Hello {username}, you have successfully logged in.");
                    //await context.PostAsync($"Response is {t}");
                    context.UserData.SetValue("Authorization_Token_ProjectManagement", t);
                    context.Done(true);
                }
            }
            else
            {
                await context.PostAsync("wrong credentials");
                context.Wait(GetUser);
            }
            

            //Conversation.UpdateContainer(
            //   builder =>
            //   {
            //       var store = new InMemoryDataStore();
            //       builder.Register(c => store)
            //              .Keyed<IBotDataStore<BotData>>(t)
            //              .AsSelf()
            //              .SingleInstance();
            //   });
            //context.Done(true);
        }

    }
}