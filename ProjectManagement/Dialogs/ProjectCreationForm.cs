using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class ProjectCreationForm
    {
        [Prompt("Enter client name:")]
        public string ClientName { get; set; }

        [Prompt("Enter Project Type:")]
        public String ProjectType { get; set; }

        [Prompt("Enter Project Manager Name:")]
        public String ProjectManager { get; set; }

        [Prompt("Enter Department Name:")]
        public String DeptName { get; set; }

        [Prompt("Select start date of project:")]
        public String StartDate { get; set; }

        [Prompt("Select end date of project:")]
        public String EndDate { get; set; }

        [Prompt("Enter Project status:")]
        public String Status { get; set; }


        public static IForm<ProjectCreationForm> ProjectForm()
        {
            return new FormBuilder<ProjectCreationForm>()
                .Message("Please Enter Following Details...")
                .Field(nameof(ClientName))
                .Field(nameof(ProjectType))
                .Field(nameof(ProjectManager))
                .Field(nameof(DeptName))
                .Field(nameof(StartDate))
                .Field(nameof(EndDate))
                .Field(nameof(Status))
                .Message("You have Filled...<br>" +
                "Client Name : {ClientName} <br>" +
                "Project Type : {ProjectType} <br>" + 
                "Project Manager Name : {ProjectManager} <br>" +
                "Department : {DeptName} <br>" + 
                "StartDate : {StartDate} <br>" + 
                "EndDate : {EndDate} <br>"+
                "Project Status : {Status}")
                .Build();
        }
    }
}