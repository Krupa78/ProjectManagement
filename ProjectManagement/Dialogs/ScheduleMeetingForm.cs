using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class ScheduleMeetingForm
    {
        public MeetingWith meetingWith;

        [Prompt("Please Enter Date for meeting")]
        public string Date { get; set; }

        [Prompt("Please Enter Time for meeting")]
        public String Time { get; set; }

        public static IForm<ScheduleMeetingForm> MeetingForm()
        {
            return new FormBuilder<ScheduleMeetingForm>()
                .Message("Please Enter Following Details to schedule a meeting...")
                .Field(nameof(meetingWith))
                .Field(nameof(Date))
                .Field(nameof(Time))
                .Build();
        }
    }

    [Serializable]
    public enum MeetingWith
    {
        BusinessAnalysts = 1,
        Testers = 2,
        ProjectManager = 3,
        Client = 4,
        Developers = 5
    }
}