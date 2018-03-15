using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Dialogs
{
    public class ProjectCreationForm
    {
        [Prompt("Please Enter your Name?")]
        public string Name { get; set; }

        [Prompt("Enter your Employee Id")]
        public String EmployeeId { get; set; }



        public static IForm<ProjectCreationForm> ProjectForm()
        {
            return new FormBuilder<ProjectCreationForm>()
                .Message("Please Enter Following Details...")
                .Field(nameof(Name))
                .Message("Hello, {Name}")
                .Field(nameof(EmployeeId))
                .Build();
        }
}