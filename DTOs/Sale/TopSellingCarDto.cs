namespace CarDealershipAPI.DTOs.Sale
{
    public class TopSellingCarDto
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }

}
