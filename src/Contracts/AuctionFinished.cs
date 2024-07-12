namespace Contracts;

public class AuctionFinished
{
    /// <summary>
    /// Indicates whether the item was sold in the auction.
    /// </summary>
    public bool ItemSold { get; set; }
    /// <summary>
    /// The unique identifier of the auction.
    /// </summary>
    public string AuctionId { get; set; }
    /// <summary>
    /// The identifier of the auction winner.
    /// </summary>
    public string Winner { get; set; }
    /// <summary>
    /// The identifier of the seller in the auction.
    /// </summary>
    public string Seller { get; set; }
    /// <summary>
    /// The final amount for which the item was sold, if applicable.
    /// </summary>
    public int? Amount { get; set; }
}