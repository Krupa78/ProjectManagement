using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class DesignationDetailsForm
    {
        public DesignationTypes designationTypes;

        [Prompt("Please Enter your Name?")]
        public string Name { get; set; }

        [Prompt("Enter your Employee Id")]
        public String EmployeeId { get; set; }

        public static IForm<DesignationDetailsForm> DesignationForm()
        {
            return new FormBuilder<DesignationDetailsForm>()
                .Message("Please Enter Following Details...")
                .Field(nameof(Name))
                .Message("Hello, {Name}")
                .Field(nameof(EmployeeId))
                .Message("Select your Designation.")
                .Field(nameof(designationTypes))
                .OnCompletion(async (context, profileForm) =>
                {
                    await context.PostAsync("Designation Selected...");
                })
                .Build();
        }

        
       
        [Serializable]
        public enum DesignationTypes
        {
            Business_Unit = 1,
            Project_Manager = 2,
            Business_Analysts = 3,
            Developers = 4,
            Client = 5
        }
    }
}