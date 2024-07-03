namespace SearchService.RequestHelpers;

/// <summary>
/// Represents the paramters for searching auctions.
/// </summary>
public class SearchParams
{
    /// <summary>
    /// The search term to filter auctions.
    /// </summary>
    public string SearchTerm { get; set; }
    /// <summary>
    /// The page number for pagination. Default is 1.
    /// </summary>
    public int PageNumber { get; set; } = 1;
    /// <summary>
    /// The page size for pagination. Default is 4.
    /// </summary>
    public int PageSize { get; set; } = 4;
    /// <summary>
    /// Filter auctions by seller.
    /// </summary>
    public string Seller { get; set; }
    /// <summary>
    /// Filter auctions by winner.
    /// </summary>
    public string Winner { get; set; }
    /// <summary>
    /// Specify the ordering of search results.
    /// </summary>
    public string OrderBy { get; set; }
    /// <summary>
    /// Specify how search results should be grouped.
    /// </summary>
    public string FilterBy { get; set; }
}