namespace CarDealershipAPI.DTOs.common
{
    public class EmployeePerformanceChartDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int SalesCount { get; set; }
        public decimal SalesValue { get; set; }
        public decimal Commission { get; set; }
    }

}
