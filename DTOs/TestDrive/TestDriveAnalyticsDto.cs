namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveAnalyticsDto
    {
        public int TotalTestDrives { get; set; }
        public int CompletedTestDrives { get; set; }
        public int CancelledTestDrives { get; set; }
        public int NoShowTestDrives { get; set; }
        public decimal CompletionRate { get; set; }
        public decimal CancellationRate { get; set; }
        public decimal NoShowRate { get; set; }
        public int InterestedCustomers { get; set; }
        public int ConvertedToSales { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal AverageCustomerRating { get; set; }
        public decimal AverageServiceRating { get; set; }
        public int IncidentCount { get; set; }
        public decimal AverageTestDriveDuration { get; set; }
        public string MostPopularCar { get; set; } = string.Empty;
        public string BestPerformingEmployee { get; set; } = string.Empty;
        public List<MonthlyTestDriveDto>? MonthlyTestDrives { get; set; }
        public List<TestDriveByStatusDto>? StatusBreakdown { get; set; }
        public List<TestDriveByCarDto>? CarPopularity { get; set; }
    }

}
