using System.Net.Http;
using System.Threading.Tasks;

namespace Satobot.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<string> GetStringFromPostAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var response = await client.PostAsync(requestUri, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
