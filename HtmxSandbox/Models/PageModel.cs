namespace HtmxSandbox.Models;

public class PageModel<T> where T : class
{
    /// <summary> Maximum number of items of this page, default is 10. </summary>
    public uint Size { get; init; } = 10;

    /// <summary> Zero-based page number. 0 = first page, 1 = second page, etc. </summary>
    public uint Number { get; set; }

    /// <summary> Total number of items in the database. </summary>
    public uint Total { get; set; }

    /// <summary> The items for the current page. </summary>
    public IEnumerable<T> Items { get; set; } = [];

    /// <summary> Zero-based index of the last page. 0 when there are no items. </summary>
    public uint Last => Total == 0 ? 0 : (Total - 1) / Size;

    /// <summary> Indicates whether the current page is the first page. </summary>
    public bool IsFirst => Number == 0;

    /// <summary> Indicates whether the current page is the last page. </summary>
    public bool IsLast => Number >= Last;
}