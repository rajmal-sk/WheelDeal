namespace Contracts;

public class AuctionUpdated
{
    /// <summary>
    /// Unique Identifer of the auction.
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Make of the item being auctioned.
    /// </summary>
    public string Make { get; set; }
    /// <summary>
    /// Model of the item being auctioned.
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// Year of the manufacture of the item being auctioned. Optional field.
    /// </summary>
    public int Year { get; set; }
    /// <summary>
    /// Color of the item being auctioned.
    /// </summary>
    public string Color { get; set; }
    /// <summary>
    /// Mileage of the item being auctioned. Optional Field.
    /// </summary>
    public int Mileage { get; set; }
}