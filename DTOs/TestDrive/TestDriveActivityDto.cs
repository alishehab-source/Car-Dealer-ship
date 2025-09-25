namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveActivityDto
    {
        public DateTime Date { get; set; }
        public string Activity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PerformedBy { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

}
