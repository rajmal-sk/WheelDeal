using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

/// <summary>
/// This consumer handles the AuctionUpdated event messages. When an AuctionUpdated message is received,
/// it maps the message to an Item model and updates the corresponding item in the MongoDB database.  
/// If the update is not acknowledged, it throws a MessageException.
/// </summary>
public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
{
    private readonly IMapper _mappper;

    // Constructor for AuctionUpdatedConsumer
    // Initializes a new instance of the AuctionUpdatedConsumer class with the specified AutoMapper instance.
    // Parameters:
    //   mapper: The AutoMapper instance for object mapping.
    public AuctionUpdatedConsumer(IMapper mappper)
    {
        this._mappper = mappper;
    }

    // The Consume method is called when an AuctionUpdated message is received.
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        Console.WriteLine(" ---> consuming auction updated : " + context.Message.Id);

        // Map the AuctionUpdated message to an Item model.
        var item = _mappper.Map<Item>(context.Message);

        // Attempt to update the item in the database using the message's ID and new values.
        var result = await DB.Update<Item>()
            .Match(a => a.ID == context.Message.Id)
            .ModifyOnly(x => new
            {
                x.Color,
                x.Make,
                x.Model,
                x.Year,
                x.Mileage
            }, item) // Update only the specified fields with the new values from the message.
            .ExecuteAsync(); // Execute the update operation asynchronously.

        // Check if the update was not acknowledged and throw a MessageException if true.
        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(AuctionUpdated), "Problem updating mongodb");
        }

    }
}