using AuctionService.Data;
using AuctionService.Entities;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

/// <summary>
/// Consumer class that handles the bid placed event.
/// </summary>
public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly AuctionDbContext _dbcontext;

    /// <summary>
    /// Initializes a new instance of the BidPlacedConsumer class with the specified database context.
    /// </summary>
    /// <param name="context">The auction database context.</param>
    public BidPlacedConsumer(AuctionDbContext context)
    {
        _dbcontext = context;
    }

    /// <summary>
    /// Consumes the BidPlaced message and updates the auction status in the database if needed.
    /// </summary>
    /// <param name="context">The consume context containing the BidPlaced message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        // Log the start of fault connection.
        Console.WriteLine("---> Consuming bid placed");

        // Retrieve the auction from the database using the AuctionId from the message.
        var auction = await _dbcontext.Auctions.FindAsync(context.Message.AuctionId);
        // If there is no current high bid, or if the bid status is "Accepted" and the new bid amount 
        // is higher than the current high bid, update the current high bid to the new bid amount.
        if (auction.CurrentHighBid == null
        || context.Message.BidStatus.Contains("Accepted") && context.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = context.Message.Amount;
            // Save the changes to the database.
            await _dbcontext.SaveChangesAsync();
        }
    }
}