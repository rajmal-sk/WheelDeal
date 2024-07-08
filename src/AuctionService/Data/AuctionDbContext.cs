using Microsoft.EntityFrameworkCore;
using AuctionService.Entities;
using MassTransit;
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

    // Override the OnModelCreating method to configure the model and entity mappings.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Call the base class's onModelCreating method to ensure any configuration
        // done by the base class is applied.
        base.OnModelCreating(builder);
        // Add the inbox state entity to the model. This is used by MassTransit to 
        // track the state of messages in the inbox, ensuring they are processed correctly.
        builder.AddInboxStateEntity();
        // Add the outbox message entity to the model. This is used by MassTransit to 
        // store the messages that are to be sent out, ensuring reliable message delivery.
        builder.AddOutboxMessageEntity();
        // Add the outbox state entity to the model. This is used by MassTransit to track
        // the state of the outbox, ensuring messages are sent only once and managing the
        // outbox state.
        builder.AddOutboxStateEntity();
    }
}