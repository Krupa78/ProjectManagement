using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZestClientApi.Repository
{
    public class SprintClient
    {
        public async Task<string> SprintDetails(string token)
        {
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:57144/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            cons.DefaultRequestHeaders.Add("Authorization", token);
            var id = new TestRequest { id = 2 };
            string proreq = JsonConvert.SerializeObject(id);
            HttpContent procontent = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage prores = cons.PostAsync("http://localhost:57144/api/Sprint/GetSprint", new StringContent(@"{""RequestJSON"":" + proreq + "}", Encoding.Default, "application/json")).Result;
            //string result = await prores.Content.ReadAsStringAsync();
            var procode = await prores.Content.ReadAsAsync<Sprint>();
            var pro = procode.ResponseJSON.ProjCode;
            string name = pro.ToString();
            return name;
        }
    }
    public class ProjectCode
    {
        public string ProjCode { get; set; }
    }
    public class Sprint
    {
        public int ProjId { get; set; }
        public string ProjCode { get; set; }
        public Sprint ResponseJSON { get; set; }
    }
}
