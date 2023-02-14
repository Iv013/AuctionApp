namespace AuctionApp.Data.Models.ModelsDTO
{
    public class AuctionDTO
    {
        public string Number { get; set; }
        public string CompetitionOrgan { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime FactDate { get; set; }
        public string Address { get; set; }
        public string AuctionType { get; set; }
        public string AuctionStatusStr { get; set; }
        public DateTime LastDateChanged { get; set; }
        public DateTime PublicationDate { get; set; }
        public LotDTO[] Lots { get; set; }
    }

}
