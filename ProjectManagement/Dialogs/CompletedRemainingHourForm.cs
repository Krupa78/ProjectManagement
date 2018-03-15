using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class CompletedRemainingHourForm
    {
        public HourTypes hourTypes;

        public static IForm<CompletedRemainingHourForm> WorkingHourForm()
        {
            return new FormBuilder<CompletedRemainingHourForm>()
                .Field(nameof(hourTypes))
                .Build();
        }
    }

    [Serializable]
    public enum HourTypes
    {
        //abc=1,
        CompletedHour = 2,
        RemainingHour = 3,
    }
}