using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApiGatewayOcelot.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiGatewayOcelot
{
    [Route("api/[controller]")]
    [ApiController]  
    public class AggregatorController : ControllerBase     
    {
        IPostService postService;
        IUserService userService;
        
        public AggregatorController(IPostService _postService, IUserService _userService)
        {
            postService = _postService;
            userService = _userService;
        }

        [HttpGet("user")]
        public async Task<IActionResult> LoadUserDetailAsync()
        {
            var posts = await postService.GetPostItems();
            var users = await userService.GetUserItems();
            var postsGroupByUser = posts.GroupBy(p => p.userId).ToDictionary(p => p.Key, gps => gps.ToList());
            var aggregatedPayload = new List<object>();
            foreach (var user in users)
            {
                var postsByUser = postsGroupByUser[user.id];
                var userWithPosts = new {id = user.id, name = user.name, post_items = postsByUser};
                aggregatedPayload.Add(userWithPosts);
            }
            return Ok(aggregatedPayload);
        }

    }
}