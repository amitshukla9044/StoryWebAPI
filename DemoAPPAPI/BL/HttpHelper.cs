using System.Text;

namespace DemoAPPAPI.BL
{
   
    public  class HttpHelper: IHttpHelper
    {
       
        private  readonly string apiBasicUri = " https://hacker-news.firebaseio.com/";

        public  async Task<T> Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }

      
    }
}
