namespace Contracts;

public class BidPlaced
{
    /// <summary>
    /// The unique identifier for the bid.
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// The unique identifier of the auction associated with the bid.
    /// </summary>
    public string AuctionId { get; set; }
    /// <summary>
    /// The unique identifier of the bidder.
    /// </summary>
    public string Bidder { get; set; }
    /// <summary>
    /// The time when the bid was placed.
    /// </summary>
    public DateTime BidTime { get; set; }
    /// <summary>
    /// The amount of the bid.
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// The status of the bid.
    /// </summary>
    public string BidStatus { get; set; }
}