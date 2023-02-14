using LinqToDB;
using LinqToDB.Data;


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
            var result = GetGuide(entity);//получаем объект, id его и флаг необходимо ли его записывать в базу или нет 
            if (result.Item3)
               await db.InsertAsync<T>(result.Item1);
            return result.Item2;
        }

        //метод нужен чтобы проверить есть ли запись которыя будет добавляться в базе и вернуть либо новый ID
        //либо ID существующей записи.
        //метод сделала абстрактым так как  в нем используется метод FirstOrDefault,
        //а в нем фильтр для каждого класса имеет разные параметры, поэтому данные метод реализован для каждого из классов таблицы
        public abstract (T, Guid, bool) GetGuide(T entity);
    }
}
