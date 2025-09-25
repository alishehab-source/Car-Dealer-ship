namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeAppointmentDto
    {
        public DateTime DateTime { get; set; }
        public string Type { get; set; } = string.Empty; // TestDrive, Meeting, Customer Visit
        public string Description { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
