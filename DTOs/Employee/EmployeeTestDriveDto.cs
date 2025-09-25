namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeTestDriveDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsConvertedToSale { get; set; }
    }

}
