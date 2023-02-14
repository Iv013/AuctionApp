using AuctionApp.Data;
using AuctionApp.Data.Models.ModelsDTO;
using AuctionApp.Data.Tables;
using AuctionApp.Data.Tables.Repository;
using AuctionApp.Data.Tables.Repository.IRepository;
using AutoMapper;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace AuctionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : Controller
    {
        public AuctionsController(IMapper mapper )

        {
            _mapper = mapper;
   
        }

        private AuctionsDatabase _database = new();
        private IMapper _mapper;


        #region Примеры

        [HttpGet("[action]")]
        public string GetFirstAuctionNumber()
        {
            return _database.Auctions.First().Number; 
        }

        [HttpGet("[action]")]
        public IList<Company> GetCompanies()
        {
            return (from company in _database.Companies
                   where company.Phone.StartsWith("+79")
                   select company).ToList();
        }

        [HttpDelete("Action")]
        public void DeleteCompany()
        {
            using (var t = _database.BeginTransaction())
            {
                var companiesToDelete = _database.Companies.Take(2).ToList();

                _database.Delete(companiesToDelete[0]);
                _database.Delete(companiesToDelete[1]);

                t.Commit();
            }
        }

        #endregion

        #region Для задания

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadJson()
        {
            string jsonText;
            var file = Request.Form.Files[0];

            using (var stream = file.OpenReadStream())
            using (var sr = new StreamReader(stream))
                jsonText = sr.ReadToEnd();

            //Десериализуем Json в созданную модель
            var result = JsonConvert.DeserializeObject<AuctionDTO>(jsonText);

            //получаем с помошью AutoMApper модель Auction для базы данных
            var auction =    _mapper.Map<AuctionDTO, Auction >(result);
            if (auction == null)
            {
                return Ok();
            }

               //Добавляем данные по аукциону в базу(если данных небыло)
               var auctionID = await _database.AuctRepo.InsertEntity(auction);

           //будем перебирать лоты данного аукциона, при отсутвии данных в базе будем заносить по лотам и компаниям
           // а также в табилцу LotCompany
            foreach (var lot in result.Lots)
            {

                List<LotCompany> lotCompanies = new List<LotCompany>(); 

                foreach (var company in lot.Companies)
                {
                    Guid guideCompanyOwnership = Guid.Empty;

                    //если у компании указана форма собственности проверяем указана ли данная запись в базе, если нет, добалвяем
                    if (company.Ownership != null)
                    {
                        //получаю ID формы собственности
                        guideCompanyOwnership = await _database.CompanyOwnership.InsertEntity(new CompanyOwnership  {   Name = company.Ownership  });
                    }

                   var companyForDB = _mapper.Map<CompanyDTO, Company>(company);
                   companyForDB.OwnershipId = guideCompanyOwnership;
                   var resultID = await  _database.CompanyRepo.InsertEntity(companyForDB);
                    //добавляю данные в лист с LotCompany, чтобы затем добавить в базу, когда получим ID лота
                    lotCompanies.Add(new LotCompany { CompanyId = resultID });
                }
                //получаем с помошью AutoMApper модель лота для базы данных
                var lotForDB =   _mapper.Map<LotDTO, Lot>(lot);

                //получаем из базы id компании победителя
                lotForDB.CompanyWinnerId = _database.CompanyRepo.FirstOrDefault(x => x.CompanyName == lot.CompanyWinner).Id;
                lotForDB.AuctionId = auctionID;
                //добавляем лот в базу(если он не был добавлен ранее)
                var idLot =await  _database.LotRepo.InsertEntity(lotForDB);

                //пробегаемся по таблицы для связи лотов и компаний, добавляем ID лотов, и добавляем запись в базу
                foreach( var lotCompany in lotCompanies)
                {
                    lotCompany.LotId = idLot;
                 await   _database.LotCompany.InsertEntity(lotCompany);
                }

            }

            return Ok();
        }

        [HttpGet("[action]/{search}")]
        public object LoadData(string search)  
        {
            //Не понял, для чего в app.tsx писать в строку поиска 123 при пустой строке. сделал костыль тут
            if (search == "123") search = "";


            // компании объеденяем с таблицей формы собственности, так как имеются одиннаковые имена компаний с разной формой
            var result = (from companies in _database.Companies
                           join ownership in _database.GetCompanyOverShip on companies.OwnershipId equals ownership.Id
                           select new { CompanyName = ownership.Name + " " + companies.CompanyName, Id= companies.Id } into companies
                          //добавляем поиск по включению символов из поиска
                          where companies.CompanyName.Contains(search)
                          //затем объеденняем с оставшимися таблицами по ключам
                          join lotCompany in _database.LotCompanies on companies.Id equals lotCompany.CompanyId
                           join lot in _database.Lots on lotCompany.LotId equals lot.Id
                           join auction in _database.Auctions on lot.AuctionId equals auction.Id
                          orderby companies.CompanyName
                          //группируем по номеру аукциона, из имени компании и лотов по аукциону получиться массив
                          group new{companies.CompanyName, lot} by  auction.Number  into g
                           select new {numberAuction= g.Key,
                           //а затем компании и лоты еще раз группируем по компаниям
                               comLot= from tab in g
                               group tab.lot by   tab.CompanyName  into hhh
                               select new { CompanyName = hhh.Key , LotsList = hhh.ToList() } }
                         
                         ).ToList(); 
            //в итоге получился лист с номером аукциона, и списком где перечиcлены компании и лист - список их лотов в данном аукционе
            //возможно заданием подразумевалось заполнить именно вью модель по данной схеме, но я сделал просто анонимным объектом
            return result;
        }
        #endregion
    }
}
