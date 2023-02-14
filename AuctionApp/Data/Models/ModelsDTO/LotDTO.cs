namespace AuctionApp.Data.Models.ModelsDTO
{
    public class LotDTO
    {
        public string ShortName { get; set; }
        public string Placement { get; set; }
        public float Area { get; set; }
        public int UseagePeriod { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public float Deposit { get; set; }
        public string CompanyWinner { get; set; }
        public float TotalPrice { get; set; }
        public string AuctionResults { get; set; }
        public string LotNum { get; set; }
        public string Resource { get; set; }
        public string Currency { get; set; }
        public float Payment { get; set; }
        public float StartPrice { get; set; }
        public CompanyDTO[] Companies { get; set; }
    }
}