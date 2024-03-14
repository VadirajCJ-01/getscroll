using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.WebRequestMethods;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel

    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        public string URL { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IConfiguration Config)
        {
            _logger = logger;
            _config = Config;
        }
        public string ScrollText { get; set; }

        public void OnGet()
        {
            ModelState.Clear();
            RedirectToPage("./Index");
        }
        public void OnPost()
        {
            try
            {
                URL = _config.GetValue<string>("MySettings:BlobLocation");
                Fetch fetch = new Fetch();
                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} | Sending request to {URL}");
               // ScrollText = fetch.GetData().Result;
               
                HttpResponseMessage response = fetch.GetData(URL).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()}| Successful Request - {response.Headers.ToString()}");
                    ScrollText =  response.Content.ReadAsStringAsync().Result;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    _logger.LogError($"{DateTime.UtcNow.ToLongTimeString()}| Unauthorized Request - {response.Headers.ToString()}");
                    ScrollText = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    _logger.LogCritical($"{DateTime.UtcNow.ToLongTimeString()}| Failed Request - {response.Headers.ToString()}");
                    ScrollText = response.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"{DateTime.UtcNow.ToLongTimeString()} | {ex.Message}");
                

            }
            }
            
    }
}
