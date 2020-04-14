using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGatewayOcelot.Service
{
    public interface IUserService
    {
        Task<List<User>> GetUserItems();
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}