namespace CarDealershipAPI.DTOs.common
{
    public class ComponentHealthDto
    {
        public string Status { get; set; } = "Healthy";
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
    }

}
