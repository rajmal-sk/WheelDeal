using Microsoft.EntityFrameworkCore;
using AuctionService.Entities;
namespace AuctionService.Data;

/// <summary>
/// The database context for the Auction service, derived from Entity Framework <see cref="DbContext"/> .
/// </summary>
public class AuctionDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuctionDbContext"/> with the specified options.
    /// </summary>
    /// <param name="options"></param>
    public AuctionDbContext(DbContextOptions options) : base(options)
    {

    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> of <see cref="Auction"/> entities. 
    /// </summary>
    public DbSet<Auction> Auctions { get; set; }
}