namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int TotalSales { get; set; }
        public decimal TotalSalesValue { get; set; }
        public decimal MonthlyTarget { get; set; }
        public decimal AchievementPercentage { get; set; }
        public decimal? PerformanceRating { get; set; }
        public int DaysWithoutSale { get; set; }
        public string? ProfileImageUrl { get; set; }
    }

}
