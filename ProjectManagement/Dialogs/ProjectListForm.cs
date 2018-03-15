using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class ProjectListForm
    {
        public ListTypes listTypes;

        public static IForm<ProjectListForm> ProjectForm()
        {
            return new FormBuilder<CompletedRemainingHourForm>()
                .Field(nameof(listTypes))
                .Build();
        }
    }
    [Serializable]
    public enum ListTypes
    { 
        CompletedProjects = 1,
        CurrentProjects = 2,
    }
}