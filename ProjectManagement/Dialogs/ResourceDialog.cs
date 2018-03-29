using Chronic;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;



namespace ProjectManagement.Dialogs
{
    [Serializable]
    public class ResourceDialog : IDialog<object>
    {
        public string username { get; private set; }
        public string password { get; private set; }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Oopss!! No resources are available.");
            context.Wait(this.Auth);
            //context.Done(true);
        }

        private async Task Auth(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // User message
            string userMessage = activity.Text;
            try
            {

                using (HttpClient client = new HttpClient())
                {

                    HttpClient cons = new HttpClient();
                    cons.BaseAddress = new Uri("http://localhost:57144/");
                    cons.DefaultRequestHeaders.Accept.Clear();
                    cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                    await context.PostAsync("Enter Username..");
                    var user = await result as Activity;
                    username = (user.Text.ToString());

                    await context.PostAsync("Enter Password..");
                    var pswd = await result as Activity;
                    password = (pswd.Text.ToString());

                    var tag = new AuthenticationRequest { UserName = username, Password = password, AuthenticationType = "form" };

                    string req = JsonConvert.SerializeObject(tag);
                    HttpContent content = new StringContent(tag.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = cons.PostAsync("http://localhost:57144/api/Authentication/UserLogin", 
                        new StringContent(@"{""RequestJSON"":" + req + "}", Encoding.Default, "application/json")).Result;

                    var data = await res.Content.ReadAsAsync<ServiceResponse>();
                    var token = data.ResponseJSON.AuthorizationToken;
                    await context.PostAsync($"{token}");



                    // await context.PostAsync($"Response is {token}");
                    if (res.IsSuccessStatusCode)
                    {
                        cons.DefaultRequestHeaders.Add("Authorization", token);
                        var id = new TestRequest { id = 2 };
                        string proreq = JsonConvert.SerializeObject(id);
                        HttpContent procontent = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
                        HttpResponseMessage prores = cons.PostAsync("http://localhost:57144/api/Products/GetProduct", new StringContent(@"{""RequestJSON"":" + proreq + "}", Encoding.Default, "application/json")).Result;

                        var prodata = await prores.Content.ReadAsAsync<Product>();
                        var details = prodata.ResponseJSON.ToString();
                        await context.PostAsync(details);


                        if (res.StatusCode == HttpStatusCode.OK)
                        {
                            var test = res.Content.ReadAsAsync<AuthenticationResponse>(new[] { new JsonMediaTypeFormatter() });
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            context.Done(true);
        }
    }

    public class TestRequest
    {
        public int id { get; set; }
    }

    public class AuthenticationResponse
    {
        public int EmpID { get; set; }
        public string AuthorizationToken { get; set; }
    }

    public class ServiceResponse
    {
        public string Status { get; set; }
        public string ServerDateTime { get; set; }
        public string ErrorList { get; set; }
        public AuthenticationResponse ResponseJSON { get; set; }
    }

    public class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthenticationType { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public Product ResponseJSON { get; set; }
    }
}

