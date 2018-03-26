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
    [LuisModel("e52b24fd-c5c1-4316-959b-f2e1291b6bd9", "1d85b742f01843d086962485f0728cd5")]
    [Serializable]
    public class DevelopersDialog : LuisDialog<object>
    {
        //1. call for None Intent
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, I don't know what you wanted.");

            await context.PostAsync("Type 'help' to show what can BOT do");
            context.Wait(MessageReceived);
        }

        //2. help dialog
        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            context.Call(new HelpDialog(), ResumeAfterGeneral);
        }

        //3. good morning
        [LuisIntent("GoodMorning")]
        public async Task GoodMorning(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Good Morning ! Have a Nice day ahead!");
            context.Wait(MessageReceived);
        }

        //4. good night
        [LuisIntent("GoodNight")]
        public async Task GoodNight(IDialogContext context, LuisResult result)
        {

            await context.PostAsync("It looks like you are too much tired today...");
            await context.PostAsync("Good Night ! Sweet Dreams!");
            context.Wait(MessageReceived);
        }

        //5. call for Greetings Intent
        [LuisIntent("Greetings")]
        public async Task Greetings(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            var message = await activity as Activity;
            //take hello from user and compare
            if(message.Text.Equals("Thank you", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Anytime");
                context.Wait(MessageReceived);
            }
            
            else
            {
                await context.PostAsync("Hey, I'm there.");
                context.Call(new HelpDialog(), ResumeAfterHelp);
            }
        }
        public async Task ResumeAfterHelp(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("How can I help you?");
            context.Wait(MessageReceived);
        }


        //6. task for today
        [LuisIntent("TaskToday")]
        public async Task TaskToday(IDialogContext context, IAwaitable<object> activity,LuisResult result)
        {
            context.Call(new TaskTodayDialog(), ResumeAfterGeneral);
        }

        //7. task for tomorrow
        [LuisIntent("TaskTomorrow")]
        public async Task TaskTomorrow(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            context.Call(new TaskTomorrowDialog(), ResumeAfterGeneral);
        }

        //8. task for next week
        [LuisIntent("TaskNextWeek")]
        public async Task TaskNextWeek(IDialogContext context, IAwaitable<object> activity, LuisResult result)
        {
            context.Call(new TaskNextWeekDialog(), ResumeAfterGeneral);
        }

        //9. sprint
        [LuisIntent("Sprint")]
        public async Task Sprint(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project for your sprint...");
            context.Call(new SprintDialog(), ResumeAfterGeneral);
        }

        //10. Sprint Start Date
        [LuisIntent("SprintStartDate")]
        public async Task SprintStartDate(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project for sprint date...");
            context.Call(new SprintStartDateDialog(), ResumeAfterGeneral);
        }

        //11. Sprint Duration
        [LuisIntent("SprintDuration")]
        public async Task SprintDuration(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project for sprint duration...");
            context.Call(new SprintDurationDialog(), ResumeAfterGeneral);
        }

        //12. phase
        [LuisIntent("Phase")]
        public async Task Phase(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project to know the current phase...");
            context.Call(new PhaseDialog(), ResumeAfterGeneral);
        }

        //13. re-open ticket
        [LuisIntent("ReOpenTicket")]
        public async Task ReOpenTicket(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Select project to know the reopened task...");
            context.Call(new ReOpenedTicketDialog(), ResumeAfterGeneral);
        }

        //14. deadline
        [LuisIntent("Deadline")]
        public async Task Deadline(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Searching for deadline...");
            context.Call(new DeadLineDialog(), ResumeAfterGeneral);
        }

        //15. Schedule meeting
        [LuisIntent("ScheduleMeeting")]
        public async Task ScheduleMeeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Scheduling Meeting...");
            context.Call(new ScheduleMeetingDialog(), ResumeAfterGeneral);
        }

        //16. working-hours
        [LuisIntent("WorkingHour")]
        public async Task WorkingHour(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Finding your Project List...");
            context.Call(new WorkingHourDialog(), ResumeAfterGeneral);
        }
       
        //17. Member Details of project
        [LuisIntent("ProjectTeam")]
        public async Task ProjectTeam(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Finding team members details for your project...");
            context.Call(new MemberDetailsDialog(), ResumeAfterGeneral);
        }

        //18. Number of Member
        [LuisIntent("NoProjectTeam")]
        public async Task NoProjectTeam(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Finding team members details for your project...");
            context.Call(new NoProjectTeamDialog(), ResumeAfterGeneral);
        }

        //19. List of project
        [LuisIntent("ListOfProject")]
        public async Task ListOfProject(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Finding your Project List...");
            context.Call(new ProjectListDialog(), ResumeAfterGeneral);
        }

        //20. List of completed project
        [LuisIntent("CompletedProjectList")]
        public async Task CompletedProjectList(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("your Completed Project List...");
            context.Call(new CompletedProjectListDialog(), ResumeAfterGeneral);
        }

        //21. List of current project
        [LuisIntent("CurrentProjectList")]
        public async Task CurrentProjectList(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("your Current Project List...");
            context.Call(new CurrentProjectListDialog(), ResumeAfterGeneral);
        }

        //22. Release
        [LuisIntent("Release")]
        public async Task Release(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("For which sprint you wanted to know the release date?");
            context.Call(new ReleaseDialog(), ResumeAfterGeneral);
        }

        //23. resources
        [LuisIntent("Resources")]
        public async Task Resources(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Searching for available Resources");
            context.Call(new ResourceDialog(), ResumeAfterGeneral);
        }

        //24. Project definition
        [LuisIntent("ProjectDetails")]
        public async Task ProjectDetails(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("select project for definition...");
            context.Call(new ProjectDefinitionDialog(), ResumeAfterGeneral);
        }

        //25. Project Initiation
        [LuisIntent("ProjectInitiation")]
        public async Task ProjectInitiation(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("select project for definition...");
            context.Call(new ProjectInitiationDialog(), ResumeAfterGeneral);
        }

        //26. Project Cost
        [LuisIntent("CostEstimation")]
        public async Task CostEstimation(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("select project for which you want to check cost...");
            context.Call(new CostEstimationDialog(), ResumeAfterGeneral);
        }

        //27.Completed task
        [LuisIntent("CompletedTask")]
        public async Task CompletedTask(IDialogContext context, LuisResult result)
        {
            context.Call(new CompletedTaskDialog(), ResumeAfterGeneral);
        }

        //28.remaining task
        [LuisIntent("RemainingTask")]
        public async Task RemainingTask(IDialogContext context, LuisResult result)
        {
            context.Call(new RemainingTaskDialog(), ResumeAfterGeneral);
        }

        //resume after all
        public async Task ResumeAfterGeneral(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("How else can I help you?");
            context.Wait(MessageReceived);
        }
    }
}