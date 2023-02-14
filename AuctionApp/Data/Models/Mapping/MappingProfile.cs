using AuctionApp.Data.Models.ModelsDTO;
using AuctionApp.Data.Tables;
using AutoMapper;

namespace AuctionApp.Data.Models.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            this.CreateMap<LotDTO, Lot>();
            this.CreateMap<AuctionDTO, Auction>();
            this.CreateMap<CompanyDTO, Company>();
        }
    }
}
