using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ZestClientApi.Repository
{
    public class TechnologyClient
    {
        public async Task<string> TechnologyDetails(string token, string code)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57144/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applocation/json"));
            client.DefaultRequestHeaders.Add("Authorization", token);
            var pro_code = new TechnologyModel { ProjCode = code };
            string proj_code = JsonConvert.SerializeObject(pro_code);
            HttpContent content = new StringContent(pro_code.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("http://localhost:57144/api/Technology/GetTechnology", new StringContent(@"{""RequestJSON"":" + proj_code + "}", Encoding.Default, "application/json")).Result;
            var tech = await response.Content.ReadAsAsync<TechnologyModel>();
            var result = tech.ProjTechnology.ToString();
            //var res = phase.ProjPhase.ToString();
            return result;
        }
    }
    public class TechnologyModel
    {
        public string ProjCode { get; set; }
        public string ProjName { get; set; }
        public string ProjTechnology { get; set; }
        public TechnologyModel ResponseJSON { get; set; }
    }
}
