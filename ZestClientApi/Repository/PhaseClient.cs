﻿using Newtonsoft.Json;
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
    public class PhaseClient
    {
        public async Task<string> PhaseDetails(string token, string code)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57144/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applocation/json"));
            client.DefaultRequestHeaders.Add("Authorization", token);
            var pro_code = new ProjectPhaseModel { ProjCode = code };
            string proj_code = JsonConvert.SerializeObject(pro_code);
            HttpContent content = new StringContent(pro_code.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("http://localhost:57144/api/Phase/GetProjectPhase", new StringContent(@"{""RequestJSON"":" + proj_code + "}", Encoding.Default, "application/json")).Result;
            var phase = await response.Content.ReadAsAsync<ProjectPhaseModel>();
            var result = phase.ProjPhase.ToString();
            //var res = phase.ProjPhase.ToString();
            return result;
        }
    }

    public class ProjectPhaseModel
    {
        public string ProjCode { get; set; }
        public string ProjName { get; set; }
        public string ProjPhase { get; set; }
        public ProjectPhaseModel ResponseJSON { get; set; }
    }
    //public class ServiceResponse
    //{
    //    public string Status { get; set; }
    //    public string ServerDateTime { get; set; }
    //    public string ErrorList { get; set; }
    //    public ProjectPhaseModel ResponseJSON { get; set; }
    //}
}
