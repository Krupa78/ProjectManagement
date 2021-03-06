﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    class HelpDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {

            await context.PostAsync("**Hi! Here is a list of stuff I can help you with:** <br>" +
                "* #Task For Today : to know about the today's completed and remaining task.* <br>" +
                "* #Sprint Details : get to know about the start date and end date of any sprint for the selected project.* <br>" +
                "* #Project List : get list of current and completed project list.* <br>" +
                "* #Project definition : give information regarding the selected project.* <br>" +
                "* #Project creation : this can help you to initiate new project.* <br>" +
                "* #Phase of project : give the current phase of project.* <br>" +
                "* #Team Details : get the information of all the team members.* <br>" +
                "* #Schedule Meeting : help to schedule meeting by selecting time and place* <br>" );
            context.Done(true);
        }

    }
}