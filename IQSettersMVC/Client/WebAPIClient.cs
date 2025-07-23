using Newtonsoft.Json;

namespace IQSettersMVC.Client
{
    public static class WebApiClient
    {
        private static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };
        public static async Task<T> Get<T>(string route) { var r = await client.GetAsync(route); return JsonConvert.DeserializeObject<T>(await r.Content.ReadAsStringAsync()); }
        public static Task<HttpResponseMessage> Post<T>(string route, T obj) { return client.PostAsJsonAsync(route, obj); }
    }

}
