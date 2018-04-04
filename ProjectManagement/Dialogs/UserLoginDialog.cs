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
        protected string UserName { get; set; }
        protected string Password { get; set; }
        protected string AuthenticationType { get; set; }

        public Task StartAsync(IDialogContext context)
        {

            context.PostAsync("Please Enter your username>>..");

            context.Wait(GetUser);

            return Task.CompletedTask;

        }
        private async Task GetUser(IDialogContext context, IAwaitable<object> result)
        {
            var user = await result as Activity;
            UserName = (user.Text);

            await context.PostAsync("Enter password:");
            context.Wait(Authentication);
        }
        private async Task Authentication(IDialogContext context, IAwaitable<object> result)
        {
            var pass = await result as Activity;
            Password = (pass.Text);
            var ac = new AuthenticationClient();
            string t = await ac.TokenCalling(UserName, Password);
            if(t==null)
            {
                await context.PostAsync("wrong credentials");
                context.Wait(GetUser);
            }
            if (t != null)
            {
                await context.PostAsync($"Response is {t}");
                context.UserData.SetValue("Token", t);
                context.Done(true);
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