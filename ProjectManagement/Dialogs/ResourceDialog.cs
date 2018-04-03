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
        public string UserName { get; private set; }
        public string PassWord { get; private set; }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Oopss!! No resources are available.");
            context.Done(true);
        }

    }
}