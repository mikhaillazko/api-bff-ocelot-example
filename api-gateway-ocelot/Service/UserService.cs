using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGatewayOcelot.Service
{
    public class UserService: IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<User>> GetUserItems()
        {
            var response = await _httpClient.GetAsync("http://jsonplaceholder.typicode.com/users");
            var jsonPayload1 = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(jsonPayload1);
        }
    }
}