namespace CarDealershipAPI.DTO.Car
{
    public class CarDetailsDto : CarResponseDto
    {
        public List<SaleHistoryDto>? SalesHistory { get; set; }
        public List<TestDriveHistoryDto>? TestDriveHistory { get; set; }
        public List<string>? InterestedCustomers { get; set; }
        public CarStatisticsDto? Statistics { get; set; }
    }

}
