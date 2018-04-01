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
    public class PhaseOfProject
    {
        public string GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57144/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applocation/json"));
            HttpResponseMessage response = client.GetAsync("api/data/forall").Result;
            
            var result = response.Content.ToString();
            return result;
        }

        /*public async Task MyAPIPost(HttpClient client)
        {
            using (client)
            {
                var result = null;
                HttpResponseMessage response = client.GetAsync("api/data/forall").Result;
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        result = response.Content.ReadAsAsync();
                    }
                }
                return result;

                //string req = JsonConvert.SerializeObject(tag);
                //HttpResponseMessage resp = await client.GetAsync("api/data/forall", new StringContent(@"{""RequestJSON"":" + req + "}", Encoding.Default, "application/json")).Result;
            }
        }*/
    }
}
