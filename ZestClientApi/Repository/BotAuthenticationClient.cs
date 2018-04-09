using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zest_Client.repository
{
    public class BotAuthenticationClient
    {
        public async Task<string> BotAuthentication(string loginID, string password)
        {
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:57144/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var tag = new BotAuthenticationModel { loginID = loginID, password = password};

            string req = JsonConvert.SerializeObject(tag);
            HttpContent content = new StringContent(tag.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage res = cons.PostAsync("http://localhost:57144/api/BotAuthentication/BotAuthenticationDetails", new StringContent(@"{""RequestJSON"":" + req + "}", Encoding.Default, "application/json")).Result;
            var data = await res.Content.ReadAsAsync<ServiceResponse>();
            var login = data.ResponseJSON.loginID;
            return login;

        }
        public class ServiceResponse
        {
            public string Status { get; set; }
            public string ServerDateTime { get; set; }
            public string ErrorList { get; set; }
            public BotAuthenticationModel ResponseJSON { get; set; }
        }
        public class BotAuthenticationModel
        {
            public string loginID { get; set; }
            public string password { get; set; }
            public BotAuthenticationModel ResponseJSON { get; set; }
        }
    }
}