using MassTransit;
using Contracts;
using AutoMapper;
using SearchService.Models;
using MongoDB.Entities;

namespace SearchService.Consumers;

/// <summary>
/// This consumer handles the AuctionCreated event messages. When an AuctionCreated message is received,
/// it maps the message to an Item model and saves it to the database. If the model name is 'Foo' it throws an ArgumentException.
/// </summary>
public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
    private readonly IMapper _mapper;

    // Constructor for AuctionCreatedConsumer.
    // Initializes a new instance of the AuctionCreatedConsumer class with the specified AutoMapper instance.
    // Parameters:
    // mapper: The AutoMapper instance for object mapping.
    public AuctionCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    // The Consume method is called when an AuctionCreated message is received.
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        // Log the receipt of the message with its ID.
        Console.WriteLine(" --> consuming auction created: " + context.Message.Id);
        // Map the AuctionCreated message to an Item model.
        var item = _mapper.Map<Item>(context.Message);
        // Check if the model name is "Foo" and throw an ArgumentException if true.
        if (item.Model == "Foo") throw new ArgumentException("Cannot sell cars with name of Foo");
        // Save the mapped Item to the database.
        await item.SaveAsync();
    }
}