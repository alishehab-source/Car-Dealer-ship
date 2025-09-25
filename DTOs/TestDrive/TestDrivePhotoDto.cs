namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDrivePhotoDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string? FilePath { get; set; }
        public string? FileUrl { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime TakenDate { get; set; }
        public string TakenBy { get; set; } = string.Empty;
    }

}
