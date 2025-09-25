namespace CarDealershipAPI.DTO.Car
{

    public class TestDriveHistoryDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CustomerRating { get; set; }
        public bool IsConvertedToSale { get; set; }
    }

}
