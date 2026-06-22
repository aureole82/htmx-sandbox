using System.ComponentModel.DataAnnotations;
using HtmxSandbox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HtmxSandbox.Controllers;

public class PostsController(PostsDbContext db) : Controller
{
    /// <summary> Returns the list of all posts. </summary>
    [HttpGet] // GET Posts
    public async Task<IActionResult> Index()
    {
        return View(await db.Posts.OrderByDescending(post => post.Created).ToListAsync());
    }

    /// <summary> Returns the create form for a new post. </summary>
    [HttpGet] // GET Posts/Create
    public IActionResult Create()
    {
        return View();
    }

    /// <summary> Creates the passed post with the allowed values. </summary>
    [HttpPost] // POST Posts/Create
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Content,Author")] DbPost post)
    {
        if (ModelState.IsValid)
        {
            db.Add(post);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(post);
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

            db.Entry(stored).CurrentValues.SetValues(post);
            await db.SaveChangesAsync();

            return PartialView("_PostRow", stored);
        }

        return PartialView("_Edit", post);
    }

    /// <summary> Validates the edited post (e.g. on each keyup) without persisting any changes. </summary>
    [HttpPost] // POST Posts/Validate/5
    public IActionResult Validate(int id, [Bind("Title,Content,Author")] PostRequest post)
    {
        return PartialView("_Edit", post);
    }
}

public record PostRequest
{
    [StringLength(120)] public required string Title { get; set; }

    public string? Content { get; set; }

    [StringLength(80, MinimumLength = 3)] public required string Author { get; set; }
}