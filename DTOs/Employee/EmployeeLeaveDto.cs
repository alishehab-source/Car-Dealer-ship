namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeLeaveDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; } = string.Empty; // Annual, Sick, Emergency
        public string Status { get; set; } = string.Empty; // Pending, Approved, Rejected
        public string? Reason { get; set; }
    }
}
