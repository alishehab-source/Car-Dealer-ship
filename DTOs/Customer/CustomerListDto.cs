namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? PreferredMake { get; set; }
        public DateTime? LastContactDate { get; set; }
        public DateTime? NextContactDate { get; set; }
        public int? Rating { get; set; }
        public DateTime CreatedDate { get; set; }


        public bool IsHotLead => Status == "Hot";
        public bool NeedsFollowUp => NextContactDate.HasValue && NextContactDate.Value <= DateTime.Now;
        public string StatusColor => Status switch
        {
            "Hot" => "#ff4444",
            "Prospect" => "#ff8800",
            "Customer" => "#00aa00",
            "Cold" => "#0066cc",
            "Lost" => "#888888",
            _ => "#666666"
        };
    }

}
