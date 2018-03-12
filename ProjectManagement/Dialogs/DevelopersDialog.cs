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
            //await context.PostAsync("You have received your sprint.");
            //context.Wait(MessageReceived);

            //check condition for sprint-A
            if (message.sprintTypes.Equals("Sprint A"))
            {
                await context.PostAsync("Finding tasks for your selected Sprint");
                context.Wait(MessageReceived);
                //context.Call(new AssignedTaskForProject(), ResumeAfterAssignedTask);
            }
            else
            {
                await context.PostAsync("select appropriate sprint");
                context.Wait(MessageReceived);
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


    }
}