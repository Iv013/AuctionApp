using LinqToDB;
using LinqToDB.Data;

namespace AuctionApp.Data.Tables.Repository
{
    public interface IRepository<T>
    {
        public DataConnection db { get;  }
        ITable<T> GetEntity();  // Получение всей таблицы
        public Task<Guid> InsertEntity(T entity); //добавление записи
        T FirstOrDefault(Func<T, bool> filter = null); //получение первого либо NULL
    }
}
