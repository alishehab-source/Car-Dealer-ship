namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeePerformanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public int TotalSales { get; set; }
        public decimal TotalSalesValue { get; set; }
        public decimal TotalCommissionEarned { get; set; }
        public decimal AverageSaleValue { get; set; }
        public DateTime? LastSaleDate { get; set; }

        public int SalesThisMonth { get; set; }
        public decimal SalesValueThisMonth { get; set; }
        public decimal CommissionThisMonth { get; set; }

        public int SalesRank { get; set; }
        public int SalesValueRank { get; set; }

        public decimal MonthlyTarget { get; set; }
        public decimal TargetAchievementPercentage { get; set; }
    }
}