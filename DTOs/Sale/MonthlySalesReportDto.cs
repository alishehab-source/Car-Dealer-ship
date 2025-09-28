namespace CarDealershipAPI.DTOs.Sale
{
    public class MonthlySalesReportDto
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCommissions { get; set; }
        public decimal AverageSaleValue { get; set; }
        public List<TopPerformerDto> TopEmployees { get; set; } = new List<TopPerformerDto>();
        public List<TopSellingCarDto> TopSellingCars { get; set; } = new List<TopSellingCarDto>();
    }

}
