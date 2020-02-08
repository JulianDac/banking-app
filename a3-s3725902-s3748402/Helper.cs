using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NwbaSystem.Web.Helper
{
    public class NwbaApi
    {
        private const string ApiBaseUri = "http://localhost:49805"; // use correct port <=== dude

        public static HttpClient InitializeClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ApiBaseUri) };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
