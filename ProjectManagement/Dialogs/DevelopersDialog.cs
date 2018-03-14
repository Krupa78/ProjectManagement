using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;
using System.Web.Http;

namespace ProjectManagement.Dialogs
{
    [LuisModel("75a5d7cb-a133-4c5b-9f1e-1629d7c9acb9", "1d85b742f01843d086962485f0728cd5")]
    [Serializable]
    public class DevelopersDialog : LuisDialog<object>
    {
        //call for None Intent
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, I don't know what you wanted.");
            context.Wait(MessageReceived);
        }

        //call for Greetings Intent
        [LuisIntent("Greetings")]
        public async Task Greetings(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            //context.Call(new GeneralDialog(), ResumeAfterGeneral);
            var message = await activity as Activity;
            //take hello from user and compare
            if(message.Text.Equals("Hello", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Hello, Welcome to the Project Management BOT.");

                //call FormFlow to take designation details from user
                var designationFormFlow = FormDialog.FromForm(DesignationDetailsForm.DesignationForm, FormOptions.PromptInStart);
                context.Call(designationFormFlow, ResumeAfterDesignationForm);
            }
            else if(message.Text.Equals("Good morning", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Good Morning, How are you?");
                context.Wait(MessageReceived);
            }
            else if(message.Text.Equals("Thank you", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Anytime");
                context.Wait(MessageReceived);
            }
            else
            {
                await context.PostAsync("Hey, I'm there.");
                context.Wait(MessageReceived);
            }
        }

        //Resume aftre Selecting Designation As Developers
        //Call FormFlow to select the Project
        private async Task ResumeAfterDesignationForm(IDialogContext context, IAwaitable<object> result)
        {
            //FormFlow for Project Selection
            await context.PostAsync("Select your project...");
            var projectFormFlow = FormDialog.FromForm(ProjectSelectionForm.ProjectForm, FormOptions.PromptInStart);
            context.Call(projectFormFlow, ResumeAfterProjectSelection);
            //await context.PostAsync(null);
        }


        //1. Call for Task Intent
        [LuisIntent("Task")]
        public async Task Task(IDialogContext context, IAwaitable<object> activity,LuisResult result)
        {
            await context.PostAsync("Finding your task...");
            context.Call(new AssignedTaskForProject(), ResumeAfterAssignedTask);
            //FormFlow to select Designation from the user
            /*var designationFormFlow = FormDialog.FromForm(DesignationDetailsForm.DesignationForm, FormOptions.PromptInStart);
            context.Call(designationFormFlow, ResumeAfterDesignationForm);*/
            //await context.PostAsync(null);
        }
        //resume after calling assigned task
        private async Task ResumeAfterAssignedTask(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("You have assigned these task for today.");
            context.Wait(MessageReceived);
        }

        /*[LuisIntent("Details")]
        public async Task Details(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("wait");
            context.Call(new InformationDialog(), this.ResumeAfterInformationDialog);
        }*/

        //2. Call FormFlow by Project Intent
        [LuisIntent("ListOfProject")]
        public async Task ListOfProject(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            await context.PostAsync("Finding your Project List...");
            var projectFormFlow = FormDialog.FromForm(ProjectSelectionForm.ProjectForm, FormOptions.PromptInStart);
            context.Call(projectFormFlow, ResumeAfterProjectSelection);
            //await context.PostAsync(null);
        }

        //Resume After Selecting Project
        private async Task ResumeAfterProjectSelection(IDialogContext context, IAwaitable<ProjectSelectionForm> result)
        {
            var message = await result; 
            await context.PostAsync("Project selected...");
            if (message.projectTypes.Equals("Face Detection"))
            {

                //var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
                //context.Call(sprintFormFlow, ResumeAfterSprint);
            }
            else
            {
                await context.PostAsync("Select project..........");
            }
            context.Wait(MessageReceived);
        }

        
        //3. call sprint detail dialog
        [LuisIntent("Sprint")]
        public async Task Sprint(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select Sprint for your task...");
            var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
            context.Call(sprintFormFlow, ResumeAfterSprint);
        }

        //resume after calling sprint detail dialog
        public async Task ResumeAfterSprint(IDialogContext context, IAwaitable<SprintDetailForm> result)
        {
            var message = await result;
            await context.PostAsync(message.sprintTypes.ToString());
            //context.Wait(MessageReceived);
          
            //check condition for sprint-A
            if (message.sprintTypes.ToString().Equals("SprintB"))
            {
                await context.PostAsync("Finding tasks for your selected Sprint B");
                //context.Wait(MessageReceived);
                context.Call(new AssignedTaskForProject(), ResumeAfterAssignedTask);
            }
            else if(message.sprintTypes.ToString().Equals("SprintC"))
            {
                await context.PostAsync("Finding tasks for your selected Sprint C");
                //context.Wait(MessageReceived);
                context.Call(new AssignedTaskForProject(), ResumeAfterAssignedTask);
            }
            else if (message.sprintTypes.ToString().Equals("SprintD"))
            {
                await context.PostAsync("Finding tasks for your selected Sprint D");
                //context.Wait(MessageReceived);
                context.Call(new AssignedTaskForProject(), ResumeAfterAssignedTask);
            }
        } 

        
        //4. call time given for the task or project dialog
        [LuisIntent("Time")]
        public async Task Time(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Searching for deadline...");
            context.Call(new TimeLineDialog(), ResumeAfterTimeLine);
        }
        //resume after calling time dialog
        private async Task ResumeAfterTimeLine(IDialogContext context, IAwaitable<object> result)
        {
            //var message = await result as Activity; 
            await context.PostAsync("Hurry up!! You don't have too much time.");
            context.Wait(MessageReceived);
        }


        //5. re-open ticket
        [LuisIntent("ReOpenTicket")]
        public async Task ReOpenTicket(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Searching for assigned ticket details...");
            context.Call(new ReOpenedTicketDialog(), ResumeAfterReOpenedTask);
        }
        //resume after calling re-open task dialog
        private async Task ResumeAfterReOpenedTask(IDialogContext context, IAwaitable<object> result)
        {
            //var message = await result as Activity; 
            await context.PostAsync("Hurry up!! You don't have too much time.");
            context.Wait(MessageReceived);
        }


        //6. release date
        [LuisIntent("Release")]
        public async Task Release(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("For which sprint you wanted to know the release date?");
            var sprintFormFlow = FormDialog.FromForm(SprintDetailForm.SprintForm, FormOptions.PromptInStart);
            context.Call(sprintFormFlow, ResumeAfterRelease);
        }
        //resume after calling re-open task dialog
        public async Task ResumeAfterRelease(IDialogContext context, IAwaitable<SprintDetailForm> result)
        {
            var message = await result;
            
            //check condition for sprint-A
            if (message.sprintTypes.ToString().Equals("SprintB"))
            {
                await context.PostAsync("Finding release date for your selected Sprint B");
                context.Call(new ReleaseDialog(), ResumeAfterGeneral);
            }
            else if (message.sprintTypes.ToString().Equals("SprintC"))
            {
                await context.PostAsync("Finding release date for your selected Sprint C");
                context.Call(new ReleaseDialog(), ResumeAfterGeneral);
            }
            else if (message.sprintTypes.ToString().Equals("SprintC"))
            {
                await context.PostAsync("Finding release date for your selected Sprint D");
                context.Call(new ReleaseDialog(), ResumeAfterGeneral);
            }   
        }

        public async Task ResumeAfterGeneral(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("How else can I help you?");
            context.Wait(MessageReceived);
        }

        //7. working-hours
        [LuisIntent("WorkingHour")]
        public async Task WorkingHour(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Finding your Project List...");
            var projectFormFlow = FormDialog.FromForm(ProjectSelectionForm.ProjectForm, FormOptions.PromptInStart);
            context.Call(projectFormFlow, ResumeAfterWoringhours);
        }
        //resume after calling re-open task dialog
        private async Task ResumeAfterWoringhours(IDialogContext context, IAwaitable<ProjectSelectionForm> result)
        {
            var message = await result; 
            //await context.PostAsync("Hurry up!! You don't have too much time.");
            //context.Wait(MessageReceived);
            //check condition for project

            if (message.projectTypes.ToString().Equals("CryptoCurrency"))
            {
                await context.PostAsync("Finding working hour for your selected project");
                context.Call(new WorkingHourDialog(), ResumeAfterGeneral);
            }
            else if (message.projectTypes.ToString().Equals("AttendanceAndPayroll"))
            {
                await context.PostAsync("Finding working hour for your selected project");
                context.Call(new WorkingHourDialog(), ResumeAfterGeneral);
            }
            else if (message.projectTypes.ToString().Equals("FaceDetection"))
            {
                await context.PostAsync("Finding working hour for your selected project");
                context.Call(new WorkingHourDialog(), ResumeAfterGeneral);
            }
            else if (message.projectTypes.ToString().Equals("ProjectManagement"))
            {
                await context.PostAsync("Finding working hour for your selected project");
                context.Call(new WorkingHourDialog(), ResumeAfterGeneral);
            }
        }


        //8. resources
        [LuisIntent("Resources")]
        public async Task Resources(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Searching for available Resources");
            context.Call(new ResourceDialog(), ResumeAfterGeneral);
        }
    }
}