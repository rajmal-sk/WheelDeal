namespace Contracts;

public class AuctionCreated
{
    /// <summary>
    /// Unique Identifer of the auction.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Reserve price set for the auction. Default to 0 if not specified.
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
    /// Current highest bid in the auction.
    /// </summary>
    public int? CurrentHighBid { get; set; }
    /// <summary>
    /// Date and time when the auction was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Date and time when the auction was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    /// <summary>
    /// Date and time when the auction is scheduled to end.
    /// </summary>
    public DateTime AuctionEnd { get; set; }
    /// <summary>
    /// Current status of the auction (Live, Finished, ReservePriceNotMet )
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// Make of the item being auctioned.
    /// </summary>
    public string Make { get; set; }
    /// <summary>
    /// Model of the item being auctioned.
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// Year of manufacture of the item being auctioned.
    /// </summary>
    public int Year { get; set; }
    /// <summary>
    /// Color of the item being auctioned.
    /// </summary>
    public string Color { get; set; }
    /// <summary>
    /// Mileage of the item being auctioned.
    /// </summary>
    public int Mileage { get; set; }
    /// <summary>
    /// URL of the image representing the item being auctioned.
    /// </summary>
    public string ImageUrl { get; set; }
}
