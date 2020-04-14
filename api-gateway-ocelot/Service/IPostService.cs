using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGatewayOcelot
{
    public interface IPostService
    {
        Task<List<Post>> GetPostItems();
    };

    public class Post
    {
        public int userId{ get; set; }
        public int id { get; set; }
        public string body{ get; set; }
        public string title{ get; set; }
        
    };
}