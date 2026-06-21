using System.ComponentModel.DataAnnotations;

namespace HtmxSandbox.Models;

public class DbPost
{
    public int Id { get; set; }

    [StringLength(120)] public required string Title { get; set; }

    public string? Content { get; set; }

    [StringLength(80, MinimumLength = 3)] public required string Author { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;
}