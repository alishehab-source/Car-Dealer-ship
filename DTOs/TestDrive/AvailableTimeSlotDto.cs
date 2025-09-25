namespace CarDealershipAPI.DTOs.TestDrive
{
    public class AvailableTimeSlotDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public string? UnavailableReason { get; set; }
    }
}
