using AuctionApp.Data.Tables.Repository.IRepository;
using LinqToDB.Data;

namespace AuctionApp.Data.Tables.Repository
{
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        public override (Auction, Guid , bool) GetGuide(Auction entity)
        {
            var auctionFromDB = this.FirstOrDefault(x => x.Number == entity.Number);
            entity.Id = (auctionFromDB is null) ? Guid.NewGuid() : auctionFromDB.Id;
            return (entity, entity.Id, auctionFromDB == null);
        }
    }
}
