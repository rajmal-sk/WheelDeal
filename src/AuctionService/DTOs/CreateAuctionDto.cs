using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for creating an auction.
/// </summary>
public class CreateAuctionDto
{
    /// <summary>
    /// Make of the item being auctioned. Required Field.
    /// </summary>
    [Required]
    public string Make { get; set; }
    /// <summary>
    /// Model of the item being auctioned. Required Field.
    /// </summary>
    [Required]
    public string Model { get; set; }
    /// <summary>
    /// Year of manufacture of the item being auctioned. Required Field.
    /// </summary>
    [Required]
    public int Year { get; set; }
    /// <summary>
    /// Color of the item being auctioned. Required Field.
    /// </summary>
    [Required]
    public string Color { get; set; }
    /// <summary>
    /// Mileage of the item being auctioned. Required Field.
    /// </summary>
    [Required]
    public int Mileage { get; set; }
    /// <summary>
    /// URL of the image representing the item being auctioned. Required Field.
    /// </summary>
    [Required]
    public string ImageUrl { get; set; }
    /// <summary>
    /// Reserve price set for the auction. Required Field.
    /// </summary>
    [Required]
    public int ReservePrice { get; set; }
    /// <summary>
    /// Date and time when the auction is scheduled to end. Required Field.
    /// </summary>
    [Required]
    public DateTime AuctionEnd { get; set; }
}