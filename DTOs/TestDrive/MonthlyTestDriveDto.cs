namespace CarDealershipAPI.DTOs.TestDrive
{
    public class MonthlyTestDriveDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public int TotalTestDrives { get; set; }
        public int CompletedTestDrives { get; set; }
        public int ConvertedToSales { get; set; }
        public decimal ConversionRate { get; set; }
    }

}
