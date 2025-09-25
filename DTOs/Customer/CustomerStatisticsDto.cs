namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerStatisticsDto
    {
        public int TotalTestDrives { get; set; }
        public int CompletedTestDrives { get; set; }
        public int TotalPurchases { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int DaysSinceFirstContact { get; set; }
        public int DaysSinceLastPurchase { get; set; }
        public decimal? AverageTestDriveRating { get; set; }
        public string CustomerLifetimeStage { get; set; } = string.Empty;
    }

}
