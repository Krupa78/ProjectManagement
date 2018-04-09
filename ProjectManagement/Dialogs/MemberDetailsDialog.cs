using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class MemberDetailsDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var projectFormFlow = FormDialog.FromForm(ProjectSelectionForm.ProjectForm, FormOptions.PromptInStart);
            context.Call(projectFormFlow, ResumeAfterForm);
        }

        public async Task ResumeAfterForm(IDialogContext context, IAwaitable<ProjectSelectionForm> result)
        {
            var message = await result;
            if (message.projectTypes.ToString().Equals("CryptoCurrency"))
            {
                var activity = await result;
                var selectedCard = await result;

                var msg = context.MakeMessage();

                var attachment = GetSelectedCard(selectedCard);
                msg.Attachments.Add(attachment);

                await context.PostAsync("Team members in Cryptocurrency: \n" +
                    "1. Developers : Maulik Patadia <br> "
                    + "employee Id : Emp001 <br>"
                    + "3 year exp <br>"
                    + "Contact : 8527419630 <br>" );

                await context.PostAsync(msg);
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
            {
                var activity = await result;
                var selectedCard = await result;

                var msg = context.MakeMessage();

                var attachment = GetSelectedCard(selectedCard);
                msg.Attachments.Add(attachment);

                await context.PostAsync("Team members in Attendance And Payroll: \n" +
                    "1. Developers : Maulik Patadia <br> "
                    + "employee Id : Emp001 <br>"
                    + "3 year exp <br>"
                    + "Contact : 8527419630 <br>");

                await context.PostAsync(msg);
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                var activity = await result;
                var selectedCard = await result;

                var msg = context.MakeMessage();

                var attachment = GetSelectedCard(selectedCard);
                msg.Attachments.Add(attachment);

                await context.PostAsync("Team members in Face Detection: \n" +
                    "1. Developers : Maulik Patadia <br> "
                    + "employee Id : Emp001 <br>"
                    + "3 year exp <br>"
                    + "Contact : 8527419630 <br>");

                await context.PostAsync(msg);
                context.Done(true);
            }
            else
            {
                var activity = await result;
                var selectedCard = await result;

                var msg = context.MakeMessage();

                var attachment = GetSelectedCard(selectedCard);
                msg.Attachments.Add(attachment);

                await context.PostAsync("Team members in Project Management: \n" +
                    "Developers : Maulik Patadia <br> "
                    + "employee Id : Emp001 <br>"
                    + "3 year exp <br>"
                    + "Contact : 8527419630 <br>");

                await context.PostAsync(msg);
                context.Done(true);
            }
        }

        private Attachment GetSelectedCard(object selectedCard)
        {
            var heroCard = new HeroCard
            {
                Title = "Member Detail",
                Text = "**Maulik Patadia**",
                
                Buttons = new List<CardAction> {

                    new CardAction(ActionTypes.OpenUrl, "Linked in profile", value: "https://www.linkedin.com/in/maulik-patadia-222761137/"),
                    //new CardAction(ActionTypes.OpenUrl, "Linked in profile", value: "https://www.linkedin.com/in/kevin-dankhara-a1202b5a/"),
                    
                }
            };

            return heroCard.ToAttachment();


        }
    }
}