﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using ProjectManagement.Dialogs;


namespace ProjectManagement
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        internal static IDialog<object> MakeRoot()
        {
            return Chain.From(() => new DevelopersDialog());
        }

        //static MessagesController()

        //{

        //    var builder = new ContainerBuilder();

        //    builder.RegisterType<DevelopersDialog>().AsImplementedInterfaces().InstancePerDependency();

        //    Conversation.UpdateContainer(
        //        builder =>
        //        {
        //            var store = new InMemoryDataStore();
        //            builder.Register(c => store)
        //                      .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
        //                      .AsSelf()
        //                      .SingleInstance();
        //        });
        //}

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, MakeRoot);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels


                IConversationUpdateActivity update = message;
                var client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());
                if (update.MembersAdded != null && update.MembersAdded.Any())
                {
                    foreach (var newMember in update.MembersAdded)
                    {
                        if (newMember.Id != message.Recipient.Id)
                        {
                            var reply = message.CreateReply();
                            var msg = message.CreateReply();
                            reply.Text = $"Hello {newMember.Name}! Welcome to the Project Management BoT.";
                            client.Conversations.ReplyToActivityAsync(reply);
                            Attachment attachment1 = new Attachment();
                            attachment1.ContentType = "image/png";
                            attachment1.ContentUrl = "F:/ZEST/BOT_Project/ProjectManagement-master/welcome.png";
                            msg.Attachments.Add(attachment1);
                            reply.Text = $"Type 'Hello' for Authentication.";
                            client.Conversations.ReplyToActivityAsync(msg);
                        }
                    }
                }
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}