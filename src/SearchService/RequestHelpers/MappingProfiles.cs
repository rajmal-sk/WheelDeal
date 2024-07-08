using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers;

/// <summary>
/// This class defines the AutoMapper mapping profiles for the SearchService.
/// </summary>
public class MappingProfiles : Profile
{
    // Maps AuctionCreated to Item.
    public MappingProfiles()
    {
        // Maps AuctionCreated to Item.
        CreateMap<AuctionCreated, Item>();
        // Maps AuctionUpdated to Item.
        CreateMap<AuctionUpdated, Item>();
    }
}