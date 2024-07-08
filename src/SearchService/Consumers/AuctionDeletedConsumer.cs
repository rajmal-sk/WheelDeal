using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

/// <summary>
/// This consumer handles the AuctionDeleted event messages. When an AuctionDeleted message is received,
/// it attempts to delete the corresponding item from the MongoDB database. If the deletion is not acknowledged,
/// it throws a MessageException.
/// </summary>

public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    // The Consume method is called when an AuctionDeleted message is received.
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        // Log the receipt of the message with its ID
        Console.WriteLine(" ---> consuming auction deleted : " + context.Message.Id);

        // Attempt to delete the item from the database using the message's ID.
        var result = await DB.DeleteAsync<Item>(context.Message.Id);

        // Check if the deletion was not acknowledged and throw a MessageException if true.
        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(AuctionUpdated), "Problem updating mongodb");
        }

    }
}