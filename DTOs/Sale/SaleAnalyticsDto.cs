namespace CarDealershipAPI.DTOs.Sale
{
    public class SaleAnalyticsDto
    {
        public DateTime Period { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCommissions { get; set; }
        public decimal AverageSaleValue { get; set; }
        public int CompletedSales { get; set; }
        public int PendingSales { get; set; }
        public int CancelledSales { get; set; }
        public Dictionary<string, int> SalesByPaymentMethod { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, decimal> RevenueByEmployees { get; set; } = new Dictionary<string, decimal>();
    }

}
