namespace CarDealershipAPI.DTOs.common
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "Info";
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ActionUrl { get; set; }
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
    }

}
