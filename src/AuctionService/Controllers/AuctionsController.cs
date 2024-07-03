using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

/// <summary>
/// API controller for managing auctions.
/// </summary>
[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AuctionController"/> class.
    /// </summary>
    /// <param name="context">The database context for auctions operations.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    public AuctionsController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of all auctions, including their associated items, ordered by the make of the item.
    /// </summary>
    /// <returns>An <see cref="AuctionResult{T}"/> containing a list of <see cref="AuctionDto"/> objects representing all auctions. </returns>
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await _context.Auctions
                        .Include(x => x.Item)
                        .OrderBy(x => x.Item.Make)
                        .ToListAsync();
        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    /// <summary>
    /// Retrieves an auction by its unique identifier. 
    /// </summary>
    /// <param name="id">The unique identifier of the auction to retreive. </param>
    /// <returns>An <see cref="AuctionResult{T}"/> containing the <see cref="AuctionDto"/> representing the auction, or
    /// a <see cref="NotFoundResult"/> if the auction with the specified ID does not exist.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (auction == null) return NotFound();
        return _mapper.Map<AuctionDto>(auction);
    }

    /// <summary>
    /// Creates a new auction using the provided auction details.
    /// </summary>
    /// <param name="auctionDto"> The details of the auction to be created. </param>
    /// <returns> An <see cref="ActionResult{T}"/> containing the created <see cref="AuctionDto"/>.
    /// If the auction creation fails, returns a <see cref="BadRequestResult"/>. </returns>
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        Console.WriteLine("Request received and the date time is: {0} ", auctionDto.AuctionEnd);
        var auction = _mapper.Map<Auction>(auctionDto);
        //TODO: add current user as seller
        auction.Seller = "test";
        _context.Auctions.Add(auction);
        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Could not save changes to the DB");
        return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, _mapper.Map<AuctionDto>(auction));
    }

    /// <summary>
    /// Updates an exisiting auction with the provided details.
    /// </summary>
    /// <param name="id">The unique identifier for the auction to be updated. </param>
    /// <param name="updateAuctionDto">The details to update the auctio with. </param>
    /// <returns>An <see cref="ActionResult"/> indicating the result of the update operation.
    /// Returns <see cref="OkObjectResult"/> if the update is successful, <see cref="NotFoundResult"/> 
    /// if the auction does not exist, or <see cref="BadRequestResult"/> if the update fails.   </returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id);
        if (auction == null) return NotFound();
        //TODO: Authenticate Seller before update
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        var result = await _context.SaveChangesAsync() > 0;
        if (result) return Ok("Updated successfully");
        return BadRequest("Error: Could not save changes");
    }

    /// <summary>
    /// Deletes an exisiting auction by its unique identifer. 
    /// </summary>
    /// <param name="id">The unique identifier of the auction to be deleted. </param>
    /// <returns> An <see cref="AuctionResult"/> indicating the result of the delete operation.
    /// Returns <see cref="OkObjectResult"/> if the auction is successfully deleted,
    /// <see cref="NotFoundResult"/> if the auction does not exist, or <see cref="BadRequestResult"/>
    /// if the deletion fails.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id); // Need to rethink about this API. Not a best idea to delete auction. 
        if (auction == null) return NotFound();
        //TODO: check seller == username
        _context.Auctions.Remove(auction);
        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Error: Auction Not Found");
        return Ok("Successfully removed the auction");
    }
}