
using AuctionApp.Data.Tables;
using AuctionApp.Data.Tables.Repository;
using AuctionApp.Data.Tables.Repository.IRepository;
using LinqToDB;
using LinqToDB.Data;

namespace AuctionApp.Data
{
    public class AuctionsDatabase : LinqToDB.Data.DataConnection
    {
        public AuctionsDatabase() : base()
        {
         
        }
        public AuctionsDatabase(string configuration ) : base(configuration)
        {
            
        }
        private IAuctionRepository _auctRepo;
        private ILotRepository _lotRepo;
        private ICompanyRepository _CompanyRepo;
        private ILotCompanyRepository _lotCompanyRepo;
        private ICompanyOwnerShipRepository _companyOwnership;
        public IAuctionRepository AuctRepo
        {
            get
            {
                if (_auctRepo == null)
                    _auctRepo = new AuctionRepository() { db = this};
                return _auctRepo;
            }
        }

        public ILotRepository LotRepo
        {
            get
            {
                if (_lotRepo == null)
                    _lotRepo = new LotRepository() { db = this };
                return _lotRepo;
            }
        }

        public ICompanyRepository CompanyRepo
        {
            get
            {
                if (_CompanyRepo == null)
                    _CompanyRepo = new CompanyRepository() { db = this };
                return _CompanyRepo;
            }
        }

        public ILotCompanyRepository LotCompany
        {
            get
            {
                if (_lotCompanyRepo == null)
                    _lotCompanyRepo = new LotCompanyRepository() { db = this };
                return _lotCompanyRepo;
            }
        }

        public ICompanyOwnerShipRepository CompanyOwnership
        {
            get
            {
                if (_companyOwnership == null)
                    _companyOwnership = new CompanyOwnershipRepository() { db = this };
                return _companyOwnership;
            }
        }


        public ITable<Auction> Auctions => this.GetTable<Auction>();
        public ITable<Lot> Lots => this.GetTable<Lot>();
        public ITable<Company> Companies => this.GetTable<Company>();
        public ITable<LotCompany> LotCompanies => this.GetTable<LotCompany>();
        public ITable<CompanyOwnership> GetCompanyOverShip => this.GetTable<CompanyOwnership>();

     



    }
}
