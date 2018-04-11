using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ZestClientApi.Repository
{
    public class ProjectDetailsClient
    {
        public async Task<ServiceResponseAllDetails> AllDetails(string token, string code)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57144/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applocation/json"));
            client.DefaultRequestHeaders.Add("Authorization", token);
            var pro_code = new ProjectDetailsModel { ProjCode = code };
            string proj_code = JsonConvert.SerializeObject(pro_code);
            HttpContent content = new StringContent(pro_code.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("http://localhost:57144/api/ProjectDetails/GetProjectDetails", new StringContent(@"{""RequestJSON"":" + proj_code + "}", Encoding.Default, "application/json")).Result;
            var details = await response.Content.ReadAsAsync<ServiceResponseAllDetails>();
            //var result = phase.ResponseJSON.ProjPhase.ToString();
            return details;
        }
    }

    public class ProjectDetailsModel
    {
        public string ProjCode { get; set; }
        public string ProjName { get; set; }
        public string ProjPhase { get; set; }
        public string ProjStatus { get; set; }
        public string ProjTechnology { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public int ProjCost { get; set; }
        public string ProjDomain { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Decimal WorkingHour { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
    }
    public class ServiceResponseAllDetails
    {
        public string Status { get; set; }
        public string ServerDateTime { get; set; }
        public string ErrorList { get; set; }
        public ProjectDetailsModel ResponseJSON { get; set; }
    }
}
