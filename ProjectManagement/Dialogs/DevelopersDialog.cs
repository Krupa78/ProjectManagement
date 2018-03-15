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
            
            var message = await activity as Activity;
            //take hello from user and compare
            if(message.Text.Equals("Hello", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Hello, Welcome to the Project Management BOT.");
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


        //1. task for today
        [LuisIntent("TaskToday")]
        public async Task TaskToday(IDialogContext context, IAwaitable<object> activity,LuisResult result)
        {
            context.Call(new TaskTodayDialog(), ResumeAfterGeneral);
        }

        //2. task for tomorrow
        [LuisIntent("TaskTomorrow")]
        public async Task TaskTomorrow(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            context.Call(new TaskTomorrowDialog(), ResumeAfterGeneral);
        }

        //3. task for next week
        [LuisIntent("TaskNextWeek")]
        public async Task TaskNextWeek(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            context.Call(new TaskNextWeekDialog(), ResumeAfterGeneral);
        }

        //4. sprint
        [LuisIntent("Sprint")]
        public async Task Sprint(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project for your sprint...");
            context.Call(new SprintDialog(), ResumeAfterGeneral);
        }

        //5. phase
        [LuisIntent("Phase")]
        public async Task Phase(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project to know the current phase...");
            context.Call(new PhaseDialog(), ResumeAfterGeneral);
        }

        //6. re-open ticket
        [LuisIntent("ReOpenTicket")]
        public async Task ReOpenTicket(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project to know the reopened task...");
            context.Call(new ReOpenedTicketDialog(), ResumeAfterGeneral);
        }

        //7. deadline
        [LuisIntent("Deadline")]
        public async Task Deadline(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Searching for deadline...");
            context.Call(new DeadLineDialog(), ResumeAfterGeneral);
        }

        //8. Schedule meeting
        [LuisIntent("ScheduleMeeting")]
        public async Task ScheduleMeeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Scheduling Meeting...");
            context.Call(new ScheduleMeetingDialog(), ResumeAfterGeneral);
        }

        //9. working-hours
        [LuisIntent("WorkingHour")]
        public async Task WorkingHour(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Finding your Project List...");
            context.Call(new WorkingHourDialog(), ResumeAfterGeneral);
        }
        //resume after calling re-open task dialog
        private async Task ResumeAfterWoringhours(IDialogContext context, IAwaitable<ProjectSelectionForm> result)
        {
            
        }






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

        


        //8. resources
        [LuisIntent("Resources")]
        public async Task Resources(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Searching for available Resources");
            context.Call(new ResourceDialog(), ResumeAfterGeneral);
        }
    }
}