namespace InvoiceManager.Pagination;

/// <summary>
/// Class for details of pagination
/// </summary>
public class PaginationMeta
{
    /// <summary>
    /// Constructor for creation of class
    /// </summary>
    /// <param name="page">Count of pages</param>
    /// <param name="pageSize"> Count of size of page</param>
    /// <param name="totalPages">Count of total pages</param>
    public PaginationMeta(int page, int pageSize, int totalPages)
    {
        Page = page;
        PageSize = pageSize;
        TotalPages = Convert.ToInt32(Math.Ceiling(1.0 * totalPages / pageSize));
    }

    /// <summary>
    /// Count of pages
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// Count of size of page
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Count of total pages
    /// </summary>
    public int TotalPages { get; }

}