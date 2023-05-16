namespace InvoiceManager.Pagination;

/// <summary>
/// Class for paginating items
/// </summary>
/// <typeparam name="TModel">Model for paginating any type item</typeparam>
public class PaginatedListDto<TModel>
{
    /// <summary>
    /// Items to be paginated
    /// </summary>
    public IEnumerable<TModel> Items { get; }

    /// <summary>
    /// Item for show pagination values
    /// </summary>
    public PaginationMeta Meta { get; }

    /// <summary>
    /// Constructor for creation of class
    /// </summary>
    /// <param name="items">Items to paginated</param>
    /// <param name="meta">Pagination details</param>
    public PaginatedListDto(IEnumerable<TModel> items, PaginationMeta meta)
    {
        Items = items;
        Meta = meta;
    }
}
