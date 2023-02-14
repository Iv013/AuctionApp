using AuctionApp.Data.Tables.Repository.IRepository;
using LinqToDB.Data;

namespace AuctionApp.Data.Tables.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public override (Company, Guid,bool) GetGuide(Company entity)
        {
            var entityFromDB = this.FirstOrDefault(x => x.CompanyName == entity.CompanyName && x.Location == entity.Location);
            entity.Id = (entityFromDB is null) ? Guid.NewGuid() : entityFromDB.Id;
            return (entity, entity.Id, entityFromDB == null);
        }
    }
}
