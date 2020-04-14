using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGatewayOcelot
{
    internal class PostService : IPostService
    {
        private readonly HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostItems()
        {
            var response = await _httpClient.GetAsync("http://jsonplaceholder.typicode.com/posts");
            var jsonPayload1 = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Post>>(jsonPayload1);
        }
    }

}