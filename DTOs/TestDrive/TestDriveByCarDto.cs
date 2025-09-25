namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveByCarDto
    {
        public string CarName { get; set; } = string.Empty;
        public int TestDriveCount { get; set; }
        public int ConvertedCount { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal AverageRating { get; set; }
    }
}
