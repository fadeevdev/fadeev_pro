using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private static readonly List<BlogPost> Posts = new()
    {
        new BlogPost
        {
            Id = 1,
            Title = "Hello world!",
            Content = "This is my first post",
        },
        new BlogPost
        {
            Id = 2,
            Title = "Blazor wasm",
            Content = "I'm creating my first Blazor WebAssembly blog",
        },
    };

    [HttpGet(Name = "GetAllPosts")]
    public ActionResult<List<BlogPost>> Get() => Posts;

    [HttpPost(Name = "CreatePost")]
    public ActionResult<BlogPost> Create(BlogPost post)
    {
        post.Id = Posts.Max(p => p.Id) + 1;
        post.CreatedAt = DateTime.UtcNow;
        Posts.Add(post);
        return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
    }
}
