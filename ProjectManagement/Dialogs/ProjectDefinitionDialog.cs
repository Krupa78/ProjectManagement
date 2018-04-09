using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class ProjectDefinitionDialog : IDialog<object>
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
                await context.PostAsync("*Project Name* : Crypro Currency<br>" +
                    "*Project Code* : DEM05" +
                    "*Client Name* : Dr. Nilesh Modi<br>" + 
                    "*Project Type* : Product<br>" + 
                    "*Current Phase* : Project Initiation<br>" +
                    "*Project Status* : On Hold<br>");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
            {
                await context.PostAsync("*Project Name* : Attendance And PayRoll<br>" +
                    "*Project Code* : DEM05" +
                    "*Client Name* : Dr. Nilesh Modi<br>" +
                    "*Project Type* : Product<br>" +
                    "*Current Phase* : Project Initiation<br>" +
                    "*Project Status* : On Hold<br>");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                await context.PostAsync("*Project Name* : Face Detection<br>" +
                    "*Project Code* : DEM05" +
                    "*Client Name* : Dr. Nilesh Modi<br>" +
                    "*Project Type* : Product<br>" +
                    "*Current Phase* : Project Initiation<br>" +
                    "*Project Status* : On Hold<br>");
                context.Done(true);
            }
            else if (message.projectTypes.ToString().Equals("ProjectManagement"))
            {
                await context.PostAsync("*Project Name* : Project Management<br>" +
                    "*Project Code* : DEM05" +
                    "**Client Name** : Dr. Nilesh Modi<br>" +
                    "**Project Type** : Product<br>" +
                    "**Current Phase** : Project Initiation<br>" +
                    "**Project Status** : On Hold<br>");
                context.Done(true);
            }
        }
    }
}