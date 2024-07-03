using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers;

/// <summary>
/// Controller for handling item search operations.
/// </summary>
[ApiController]
[Route("api/search")]
public class SearchController : Controller
{

    /// <summary>
    /// Retrieves a paginated list of items based on search parameters.
    /// </summary>
    /// <param name="searchParams">Search parameters including searchTerm, PageNumber, PageSize, Search, Winner, OrderBy, and FilterBy.</param>
    /// <returns>An <see cref="ActionResult"/> containing a paginated list of items matching the search criteria.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        // Create a paged search query for the Item collection.
        var query = DB.PagedSearch<Item, Item>();

        // Apply full-text search if searchTerm is provided.
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        // Sort query based on OrderBy parameter.
        query = searchParams.OrderBy switch
        {
            "make" => query.Sort(x => x.Ascending(a => a.Make)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
        };

        // Filter query based on FilterBy parameter.
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow),
            _ => query.Match(x => x.AuctionEnd > DateTime.Now)
        };

        // Match query by seller if Seller parameter is provided.
        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        // Match query by winner if Winner parameter is provided.
        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }

        // Set page number and page size for pagination.
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        // Execute the query asynchronously and retrieve results.
        var result = await query.ExecuteAsync();

        // Return results along with pagination information.
        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}