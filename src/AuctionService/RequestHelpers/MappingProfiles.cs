using System.Xml;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers;

/// <summary>
/// This class defines the AutoMapper mapping profiles for the AuctionService.
/// </summary>
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

        // Maps AuctionDto to AuctionCreated.
        CreateMap<AuctionDto, AuctionCreated>();

        // Maps properties from Auction to AuctionUpdated, including properties form Item.
        CreateMap<Auction, AuctionUpdated>().IncludeMembers(x => x.Item);

        // Maps Item to AuctionUpdated.
        CreateMap<Item, AuctionUpdated>();
    }
}