using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Maps properties from Auction to AuctionDto, including properties form Item.
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);

        // Maps Item to AuctionDto.
        CreateMap<Item, AuctionDto>();

        // Maps CreateAuctionDto to Auction, mapping its properties to Auction's Item property.
        CreateMap<CreateAuctionDto, Auction>().ForMember(d => d.Item, o => o.MapFrom(s => s));

        // Maps CreateAuctionDto to Item.
        CreateMap<CreateAuctionDto, Item>();
    }
}