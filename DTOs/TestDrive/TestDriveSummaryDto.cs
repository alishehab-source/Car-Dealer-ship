namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveSummaryDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CustomerRating { get; set; }
        public bool IsInterestedInPurchase { get; set; }
        public bool IsConvertedToSale { get; set; }
        public string FollowUpPriority { get; set; } = string.Empty;
        public bool NeedsFollowUp { get; set; }
        public int DaysUntilFollowUp { get; set; }
    }

}
