namespace WebApplication1
{
    public class Fetch
    {
        public string ScrollText { get; set; }

        public async Task<string> GetData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://techconcs1.blob.core.windows.net/testcontainer01/openinggrawl.txt");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
