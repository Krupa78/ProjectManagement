using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class SprintDetailForm
    {
        public SprintTypes sprintTypes;

        public static IForm<SprintDetailForm> SprintForm()
        {
            return new FormBuilder<SprintDetailForm>()
                .Field(nameof(sprintTypes))
                .Build();
        } 
    }

    [Serializable]
    public enum SprintTypes
    {
        SprintA = 1,
        SprintB = 2,
        SprintC = 3,
        SprintD = 4
    }
}