using Microsoft.AspNetCore.Http.Features;

namespace AuctionService.Entities;

/// <summary>
/// Represent an auction entity in the auction service.
/// </summary>
public class Auction
{
    /// <summary>
    /// Unique identifier of the auction.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Reserve price set for the auction. Defaults to 0 if not specified.
    /// </summary>
    public int ReservePrice { get; set; } = 0;
    /// <summary>
    /// Name of the seller who created the auction.
    /// </summary>
    public string Seller { get; set; }
    /// <summary>
    /// Name of the winner of the auction, if it has been sold.
    /// </summary>
    public string Winner { get; set; }
    /// <summary>
    /// Amount for which the item was sold in the auction, if sold.
    /// </summary>
    public int? SoldAmount { get; set; }
    /// <summary>
    /// Current highest bid amount in the auction.
    /// </summary>
    public int? CurrentHighBid { get; set; }
    /// <summary>
    /// Date and time when the auction was created. Defaults to the current UTC time.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Date and time when the auction was last updated. Defaults to the current UTC time.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Date and time when the auction is scheduled to end. Defaults to the current UTC time.
    /// </summary>
    public DateTime AuctionEnd { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Current status of the auction.
    /// </summary>
    public Status Status { get; set; }
    /// <summary>
    /// The item being auctioned.
    /// </summary>
    public Item Item { get; set; }
}