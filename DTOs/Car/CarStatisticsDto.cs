namespace CarDealershipAPI.DTO.Car
{

    public class CarStatisticsDto
    {
        public int TotalViews { get; set; }
        public int TotalTestDrives { get; set; }
        public int CompletedTestDrives { get; set; }
        public decimal ConversionRate { get; set; }
        public int DaysOnMarket { get; set; }
        public decimal? AverageTestDriveRating { get; set; }
    }

}
