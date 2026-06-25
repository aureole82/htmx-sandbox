using System.ComponentModel.DataAnnotations;
using HtmxSandbox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HtmxSandbox.Controllers;

public class PostsController(PostsDbContext db) : Controller
{
    private const string PageQuery = "page";

    /// <summary> Returns the list of all posts. </summary>
    [HttpGet] // GET Posts?page=2
    public async Task<IActionResult> Index([FromQuery(Name = PageQuery)] uint pageIndex, string? author = null)
    {
        var isCallback = Request.Query.ContainsKey(PageQuery);

        IQueryable<DbPost> query = db.Posts;
        if (!string.IsNullOrEmpty(author))
        {
            isCallback = true;
            query = query.Where(post => post.Author.Contains(author));
        }

        var totalPosts = await query.CountAsync();

        var page = new PageModel<DbPost>
        {
            Total = (uint)totalPosts
        };

        var posts = await query
                .OrderByDescending(post => post.Created)
                .Skip((int)(pageIndex * page.Size))
                .Take((int)page.Size)
                .ToListAsync()
            ;

        page.Items = posts;
        page.Number = pageIndex;

        return isCallback
                ? PartialView("_PostPageWithBar", page) // pageIndex=1 → partial page load.
                : View(page) // Initial page load.
            ;
    }

    /// <summary> Returns the create form for a new post. </summary>
    [HttpGet] // GET Posts/Create
    public IActionResult Create()
    {
        return PartialView("_Create", new PostRequest { Title = string.Empty, Author = string.Empty });
    }

    /// <summary> Creates the passed post with the allowed values. </summary>
    [HttpPost] // POST Posts/Create
    public async Task<IActionResult> Create([Bind("Title,Content,Author")] PostRequest post)
    {
        if (ModelState.IsValid)
        {
            var stored = new DbPost
            {
                Title = post.Title,
                Content = post.Content,
                Author = post.Author
            };
            db.Add(stored);
            await db.SaveChangesAsync();
            return PartialView("_PostRow", stored);
        }

        return PartialView("_Create", post);
    }

    /// <summary> Returns the delete confirmation dialog for the requested post. </summary>
    [HttpGet] // GET Posts/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var stored = await db.Posts
            .FirstOrDefaultAsync(post => post.Id == id);
        if (stored == null)
        {
            return NotFound();
        }

        return PartialView("_Delete", stored);
    }

    /// <summary> Deletes the request post finally in a fire-and-forget manner. </summary>
    [HttpDelete] // DELETE Posts/Delete/5
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Fire and forget deletion: No need to call SaveChanges().
        var count = await db.Posts
                .Where(post => post.Id == id)
                .ExecuteDeleteAsync()
            ;
        if (count == 0)
        {
            return NotFound();
        }

        return Ok(); // Don't return NoContent() [204] → hx-swap="delete" won't work!
    }

    /// <summary> Returns the details view for the requested post. </summary>
    [HttpGet] // GET Posts/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var stored = await db.Posts
            .FirstOrDefaultAsync(post => post.Id == id);
        if (stored == null)
        {
            return NotFound();
        }

        return PartialView("_Details", stored);
    }

    /// <summary> Returns the edit form for the requested post. </summary>
    [HttpGet] // GET Posts/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var model = await db.Posts
                .Where(post => post.Id == id)
                .Select(post => new PostRequest
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    Author = post.Author
                })
                .FirstOrDefaultAsync()
            ;
        if (model == null)
        {
            return NotFound();
        }

        return PartialView("_Edit", model);
    }

    /// <summary> Updates the requested post only with the changeable values. </summary>
    [HttpPost] // POST Posts/Edit/5
    public async Task<IActionResult> Edit(int id, [Bind("Title,Content,Author")] PostRequest post)
    {
        if (ModelState.IsValid)
        {
            var stored = await db.Posts.FindAsync(id);
            if (stored is null)
            {
                return NotFound();
            }

            post.Id = id;
            db.Entry(stored).CurrentValues.SetValues(post);
            await db.SaveChangesAsync();

            return PartialView("_PostRow", stored);
        }

        return PartialView("_Edit", post);
    }

    /// <summary> Validates the edited post (e.g. on each keyup) without persisting any changes. </summary>
    [HttpPost] // POST Posts/Validate
    public IActionResult Validate([Bind("Id,Title,Content,Author")] PostRequest post)
    {
        return PartialView(post.Id is null ? "_Create" : "_Edit", post);
    }
}

public record PostRequest
{
    public int? Id { get; set; } // null for new posts, otherwise the existing post's primary key.
    [StringLength(120)] public required string Title { get; set; }
    public string? Content { get; set; }
    [StringLength(80, MinimumLength = 3)] public required string Author { get; set; }
}