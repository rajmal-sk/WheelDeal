using AuctionService.Data;
using AuctionService.Entities;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

/// <summary>
/// Consumer class that handles the completion of an auction.
/// </summary>
public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _dbcontext;

    /// <summary>
    /// Initializes a new instance of the AuctionFinishedConsumer class with the specified database context.
    /// </summary>
    /// <param name="context">The auction database context.</param>
    public AuctionFinishedConsumer(AuctionDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    /// <summary>
    /// Consumes the AuctionFinished message and updates the auction status in the database.
    /// </summary>
    /// <param name="context">The consume context containing the AuctionFinished message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        // Log the start of fault connection.
        Console.WriteLine("---> Consuming auction finished");

        // Retrieve the auction from the database using the AuctionId from the message.
        var auction = await _dbcontext.Auctions.FindAsync(context.Message.AuctionId);
        // If the item was sold, update the auction's winner and sold amount.
        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;
        }
        // Update the auction status based on whether the sold amount met the reserve price.
        auction.Status = auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReservePriceNotMet;
        // Save the changes to the database.
        await _dbcontext.SaveChangesAsync();
    }
}