namespace eMarketDomain.Models
{
    public class Review
    {
        public decimal Id { get; set; }
        public string Description { get; set; }
        public decimal? Rating { get; set; }
        public decimal IdUser { get; set; }
        public decimal IdProduct { get; set; }
    }
}
