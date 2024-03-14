using System.Configuration;
using System.Xml;

namespace WebApplication1
{
    public class Fetch ()
    {
        public string ScrollText { get; set; }

        //private readonly IConfiguration _configuration;
        //public Fetch(IConfiguration config) {

        //    _configuration = config;
        //}
        public async Task<HttpResponseMessage> GetData(string URL)
        {
            var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://techconcs1.blob.core.windows.net/testcontainer01/openinggrawl.txt");
            var request = new HttpRequestMessage(HttpMethod.Get, URL);
            var response = await client.SendAsync(request);
            return response;
//            response.EnsureSuccessStatusCode();
           
        }
    }
}
