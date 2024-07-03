namespace AuctionService.Entities;

/// <summary>
/// Enum defining possible status for an auction
/// </summary>
public enum Status
{
    /// <summary>
    /// Auction is currently active and ongoing.
    /// </summary>
    Live,
    /// <summary>
    /// Auction has concluded successfully.
    /// </summary>
    Finished,
    /// <summary>
    /// Auction ended without meeting the reserve price.
    /// </summary>
    ReservePriceNotMet
}