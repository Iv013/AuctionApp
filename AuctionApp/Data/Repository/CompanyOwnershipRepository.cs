using AuctionApp.Data.Tables.Repository.IRepository;
using LinqToDB.Data;
using LinqToDB.Tools;

namespace AuctionApp.Data.Tables.Repository
{
    public class CompanyOwnershipRepository : Repository<CompanyOwnership>, ICompanyOwnerShipRepository
    {
        public override (CompanyOwnership, Guid, bool) GetGuide(CompanyOwnership entity)
        {
            var entityFromDB = this.FirstOrDefault(x => x.Name == entity.Name);
            entity.Id = (entityFromDB is null) ? Guid.NewGuid() : entityFromDB.Id;
            return (entity, entity.Id, entityFromDB == null);
        }
    }
}
