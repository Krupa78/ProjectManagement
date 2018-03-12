using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class ProjectSelectionForm
    {
        public ProjectTypes projectTypes;

        public static IForm<ProjectSelectionForm> ProjectForm()
        {
            return new FormBuilder<ProjectSelectionForm>()
                .Field(nameof(projectTypes))
                .Build();
        }
    }

    [Serializable]
    public enum ProjectTypes
    {
        CryptoCurrency=1,
        FaceDetection=2,
        AttendanceAndPayroll=3,
        ProjectManagement=4
    }

}