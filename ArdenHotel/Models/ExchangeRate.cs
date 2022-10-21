namespace ArdenHotel.Models
{
    public class ExchangeRate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? Buying { get; set; }
        public decimal? Selling { get; set; }
    }
}
