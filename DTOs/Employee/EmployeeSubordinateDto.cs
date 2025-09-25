namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeSubordinateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int TotalSales { get; set; }
        public decimal? PerformanceRating { get; set; }
    }

}
