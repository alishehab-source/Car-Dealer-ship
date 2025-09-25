namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerTestDriveDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CustomerRating { get; set; }
        public bool IsConvertedToSale { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
    }

}
