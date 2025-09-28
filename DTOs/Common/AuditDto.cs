namespace CarDealershipAPI.DTOs.common
{
    public class AuditDto
    {
        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public Dictionary<string, object> Changes { get; set; } = new Dictionary<string, object>();
    }

}
