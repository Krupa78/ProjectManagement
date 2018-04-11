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
    class ProjectDefinitionDialog : IDialog<object>
    {
        public string ProjectCode { get; set; }

        public async Task StartAsync(IDialogContext context)
        {

            context.PostAsync("**Enter the project code**");
            context.Wait(this.GetAllDetails);
        }

        public async Task GetAllDetails(IDialogContext context, IAwaitable<object> result)
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

                    await context.PostAsync("**Project Details::** <br>"
                        + $"*Project Code : {details.ResponseJSON.ProjCode}* <br>"
                        + $"*Project Name : {details.ResponseJSON.ProjName}* <br>"
                        + $"*Phase : {details.ResponseJSON.ProjPhase}* <br>"
                        + $"*Status : {details.ResponseJSON.ProjStatus}* <br>"
                        + $"*Technology : {details.ResponseJSON.ProjTechnology}* <br>"
                        + $"*Client : {details.ResponseJSON.CompanyName}* <br>"
                        + $"*Cost : {details.ResponseJSON.ProjCost}* <br>"
                        + $"*Domain : {details.ResponseJSON.ProjDomain}* <br>"
                        + $"*Start Date : {details.ResponseJSON.StartDate.ToLongDateString()}* <br>"
                        + $"*End Date : {details.ResponseJSON.EndDate.ToLongDateString()}* <br>"
                        + $"*Total Hours : {details.ResponseJSON.WorkingHour}* <br>");

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

        ////public async Task StartAsync(IDialogContext context)
        ////{
        ////    var projectFormFlow = FormDialog.FromForm(ProjectSelectionForm.ProjectForm, FormOptions.PromptInStart);
        ////    context.Call(projectFormFlow, ResumeAfterForm);
        ////}

        ////public async Task ResumeAfterForm(IDialogContext context, IAwaitable<ProjectSelectionForm> result)
        ////{
        ////    var message = await result;
        ////    if (message.projectTypes.ToString().Equals("CryptoCurrency"))
        ////    {
        ////        await context.PostAsync("*Project Name* : Crypro Currency<br>" +
        ////            "*Project Code* : DEM05" +
        ////            "*Client Name* : Dr. Nilesh Modi<br>" + 
        ////            "*Project Type* : Product<br>" + 
        ////            "*Current Phase* : Project Initiation<br>" +
        ////            "*Project Status* : On Hold<br>");
        ////        context.Done(true);
        ////    }
        ////    else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
        ////    {
        ////        await context.PostAsync("*Project Name* : Attendance And PayRoll<br>" +
        ////            "*Project Code* : DEM05" +
        ////            "*Client Name* : Dr. Nilesh Modi<br>" +
        ////            "*Project Type* : Product<br>" +
        ////            "*Current Phase* : Project Initiation<br>" +
        ////            "*Project Status* : On Hold<br>");
        ////        context.Done(true);
        ////    }
        ////    else if (message.projectTypes.ToString().Equals("FaceDetection"))
        ////    {
        ////        await context.PostAsync("*Project Name* : Face Detection<br>" +
        ////            "*Project Code* : DEM05" +
        ////            "*Client Name* : Dr. Nilesh Modi<br>" +
        ////            "*Project Type* : Product<br>" +
        ////            "*Current Phase* : Project Initiation<br>" +
        ////            "*Project Status* : On Hold<br>");
        ////        context.Done(true);
        ////    }
        ////    else if (message.projectTypes.ToString().Equals("ProjectManagement"))
        ////    {
        ////        await context.PostAsync("*Project Name* : Project Management<br>" +
        ////            "*Project Code* : DEM05" +
        ////            "**Client Name** : Dr. Nilesh Modi<br>" +
        ////            "**Project Type** : Product<br>" +
        ////            "**Current Phase** : Project Initiation<br>" +
        ////            "**Project Status** : On Hold<br>");
        ////        context.Done(true);
        ////    }
        ////}
    }
}