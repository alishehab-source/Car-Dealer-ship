namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeStatisticsDto
    {
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCommission { get; set; }
        public decimal AverageSaleValue { get; set; }
        public int TotalTestDrives { get; set; }
        public int ConvertedTestDrives { get; set; }
        public decimal ConversionRate { get; set; }
        public int TotalCustomers { get; set; }
        public int RepeatCustomers { get; set; }
        public decimal CustomerRetentionRate { get; set; }
        public decimal MonthlyAverageRevenue { get; set; }
        public int BestSalesMonth { get; set; }
        public int WorkingDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal AttendanceRate { get; set; }
    }

}
