namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveListDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CustomerRating { get; set; }
        public bool IsInterestedInPurchase { get; set; }
        public bool IsConvertedToSale { get; set; }
        public bool HasIncident { get; set; }
        public string FollowUpPriority { get; set; } = string.Empty;

        public bool IsCompleted => Status == "Completed";
        public bool IsScheduled => Status == "Scheduled";
        public bool IsOverdue => Status == "Scheduled" && ScheduledDateTime < DateTime.Now;
        public bool NeedsAttention => HasIncident || (IsCompleted && IsInterestedInPurchase && !IsConvertedToSale);
        public string StatusColor => Status switch
        {
            "Completed" => "#00aa00",
            "Scheduled" => "#0066cc",
            "InProgress" => "#ff8800",
            "Cancelled" => "#ff4444",
            "NoShow" => "#cc0000",
            "Rescheduled" => "#9966cc",
            _ => "#666666"
        };
        public string PriorityColor => FollowUpPriority switch
        {
            "Urgent" => "#ff4444",
            "High" => "#ff8800",
            "Medium" => "#0066cc",
            "Low" => "#00aa00",
            _ => "#666666"
        };
    }

}
