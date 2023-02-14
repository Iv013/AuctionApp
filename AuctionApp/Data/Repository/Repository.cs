using LinqToDB;
using LinqToDB.Data;
using System.Linq.Expressions;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace AuctionApp.Data.Tables.Repository
{
   public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        public  DataConnection db { get; set; }

    

        public ITable<T> GetEntity()
        {
           return db.GetTable<T>();
        }


        public T FirstOrDefault(Func<T, bool> filter = null)
        {

            return db.GetTable<T>().FirstOrDefault(filter);
        }

        public async  Task<Guid> InsertEntity(T entity)
        {
            var result = GetGuide(entity);
            if (result.Item3)
               await db.InsertAsync<T>(result.Item1);
            return result.Item2;
        }


        public abstract (T, Guid, bool) GetGuide(T entity);
    }
}
