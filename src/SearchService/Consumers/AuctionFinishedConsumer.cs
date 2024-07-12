
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

/// <summary>
/// Consumer class that handles the completion of an auction.
/// </summary>
public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
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
        var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);
        // If the item was sold, update the auction's winner and sold amount.
        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = (int)context.Message.Amount;
        }
        // Update the auction status based on whether the sold amount met the reserve price.
        auction.Status = "Finished";
        // Save the changes to the database.
        await auction.SaveAsync();
    }
}