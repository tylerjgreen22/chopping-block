using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        // Private readonly field for the data context
        private readonly DataContext _context;

        // Set local context field to DataContext via dependency injection to allow operations on database
        public PostsController(DataContext context)
        {
            _context = context;
        }

        /* All routes are async and return non blocking tasks to allow program to continue operation while waiting for tasks to complete */

        // Route to retrieve posts from DB and return list of posts.
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        // Route to retrieve single post from DB and return the post.
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            return await _context.Posts.FindAsync(id);
        }
    }
}