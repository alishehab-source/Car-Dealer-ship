namespace CarDealershipAPI.DTOs.TestDrive
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

}
