namespace CarDealershipAPI.DTOs.common
{
    public class TopSellingBrandDto
    {
        public string Brand { get; set; } = string.Empty;
        public int SalesCount { get; set; }
        public decimal Revenue { get; set; }
        public decimal Percentage { get; set; }
    }

}
