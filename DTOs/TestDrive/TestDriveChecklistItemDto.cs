namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveChecklistItemDto
    {
        public int Id { get; set; }
        public string Item { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // PreDrive, DuringDrive, PostDrive
        public bool IsChecked { get; set; }
        public string? Notes { get; set; }
        public DateTime? CheckedDate { get; set; }
        public string? CheckedBy { get; set; }
    }

}
