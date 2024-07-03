using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs;
/// <summary>
/// Data Transfer Object (DTO) for updating the auction
/// </summary>
public class UpdateAuctionDto
{
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
    public int? Year { get; set; }
    /// <summary>
    /// Color of the item being auctioned.
    /// </summary>
    public string Color { get; set; }
    /// <summary>
    /// Mileage of the item being auctioned. Optional Field.
    /// </summary>
    public int? Mileage { get; set; }
}