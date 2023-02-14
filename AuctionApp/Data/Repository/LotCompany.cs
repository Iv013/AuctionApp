using AuctionApp.Data.Tables.Repository.IRepository;
using LinqToDB.Data;

namespace AuctionApp.Data.Tables.Repository
{
    public class LotCompanyRepository : Repository<LotCompany>, ILotCompanyRepository
    {
        public override (LotCompany, Guid, bool) GetGuide(LotCompany entity)
        {
            var entityFromDB = this.FirstOrDefault(x => x.CompanyId == entity.CompanyId && x.LotId == entity.LotId);
            entity.Id = (entityFromDB is null) ? Guid.NewGuid() : entityFromDB.Id;
            return (entity, entity.Id, entityFromDB == null);
        }
    }
}
