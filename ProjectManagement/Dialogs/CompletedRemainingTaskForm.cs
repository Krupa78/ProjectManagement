using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class CompletedRemainingTaskForm
    {
        public TaskTypes taskTypes;

        public static IForm<CompletedRemainingTaskForm> TaskForm()
        {
            return new FormBuilder<CompletedRemainingTaskForm>()
                .Field(nameof(taskTypes))
                .Build();
        }
    }

    [Serializable]
    public enum TaskTypes
    {
        CompletedTask = 1,
        RemainingTask = 2,
    }
}