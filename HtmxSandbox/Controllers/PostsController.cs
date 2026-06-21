using System.ComponentModel.DataAnnotations;
using HtmxSandbox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HtmxSandbox.Controllers;

public class PostsController(PostsDbContext db) : Controller
{
    // GET: Posts
    public async Task<IActionResult> Index()
    {
        return View(await db.Posts.OrderByDescending(post => post.Created).ToListAsync());
    }

    // GET: Posts/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var stored = await db.Posts
            .FirstOrDefaultAsync(post => post.Id == id);
        if (stored == null)
        {
            return NotFound();
        }

        return View(stored);
    }

    // GET: Posts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Posts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
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

    // GET: Posts/Edit/5
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

        return View(model);
    }

    // POST: Posts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
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

            return RedirectToAction(nameof(Index));
        }

        return View(post);
    }

    // GET: Posts/Delete/5
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

    // DELETE: Posts/Delete/5
    [HttpDelete]
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
}

public record PostRequest
{
    [StringLength(120)] public required string Title { get; set; }

    public string? Content { get; set; }

    [StringLength(80, MinimumLength = 3)] public required string Author { get; set; }
}