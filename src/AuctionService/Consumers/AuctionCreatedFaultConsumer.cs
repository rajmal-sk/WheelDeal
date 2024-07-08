using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

/// <summary>
/// This consumer handles fault messages related to AuctionCreated events.
/// When an AuctionCreated message fails, this consumer processes the fault message, logs the error,
/// and takes specific actions based on the type of the exception encountered. 
/// </summary>
public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    // The consume method is called when a fault message for AuctionCreated is received.
    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        // Log the start of fault connection.
        Console.WriteLine("---> Consuming faulty creation");

        // Get the first exception from the list of exceptions in the fault message.
        var exception = context.Message.Exceptions.First();

        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Model = "Frontier";
            // Republish the modified AuctionCreated message. 
            await context.Publish(context.Message.Message);
        }
        else
        {
            // Log a message indicating an unknown exception type was encountered
            // TODO: Implement logic to display this error on a dashboard
            Console.WriteLine("Unknown exception. Display error message in the dashboard (TODO)");
        }
    }
}

