using AuctionApp.Data.Tables.Repository.IRepository;
using LinqToDB.Data;

namespace AuctionApp.Data.Tables.Repository
{
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        public override (Lot, Guid, bool) GetGuide(Lot entity)
        {
            var entityFromDB = this.FirstOrDefault(x => x.FullName == entity.FullName);
            entity.Id = (entityFromDB is null) ? Guid.NewGuid() : entityFromDB.Id;
            return (entity, entity.Id, entityFromDB == null);
        }
    }
}
