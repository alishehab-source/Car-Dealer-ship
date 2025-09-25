namespace CarDealershipAPI.DTO.Car
{
    public class CarSummaryDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty;
        public int DaysOnLot { get; set; }
        public int TestDriveCount { get; set; }
        public int InterestedCustomers { get; set; }
        public DateTime? LastActivity { get; set; }
    }

}
