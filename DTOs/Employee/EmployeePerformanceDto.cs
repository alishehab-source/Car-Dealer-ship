namespace CarDealershipAPI.DTOs.Employee
{

    public class EmployeePerformanceDto
    {
        public DateTime Date { get; set; }
        public decimal Rating { get; set; }
        public string? Comments { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty; // Monthly, Quarterly, Annual
    }

}
