using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;

// Specifies the table name in the database for this entity
[Table("Items")]
public class Item
{
    /// <summary>
    /// Unique identifer for the item.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Manufacture of the item.
    /// </summary>
    public string Make { get; set; }
    /// <summary>
    /// Model name of the item.
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// Year of manufacture of the item.
    /// </summary>
    public int Year { get; set; }
    /// <summary>
    /// Color of the item.
    /// </summary>
    public string Color { get; set; }
    /// <summary>
    /// Mileage of the item.
    /// </summary>
    public int Mileage { get; set; }
    /// <summary>
    /// URL of the image representing the item
    /// </summary>
    public string ImageUrl { get; set; }
    /// <summary>
    /// Navigating property representing the auction associated with the item.
    /// </summary>
    public Auction Auction { get; set; }
    /// <summary>
    /// Foreign key referencing the Auction entity.
    /// </summary>
    public Guid AuctionId { get; set; }

}