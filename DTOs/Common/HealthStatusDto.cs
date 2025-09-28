namespace CarDealershipAPI.DTOs.common
{
    public class HealthStatusDto
    {
        public string Status { get; set; } = "Healthy";
        public DateTime CheckedAt { get; set; } = DateTime.Now;
        public Dictionary<string, ComponentHealthDto> Components { get; set; } = new Dictionary<string, ComponentHealthDto>();
        public TimeSpan TotalDuration { get; set; }
    }

}
