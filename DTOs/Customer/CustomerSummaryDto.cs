namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public int DaysSinceCreated { get; set; }
        public int DaysSinceLastContact { get; set; }
        public int TestDriveCount { get; set; }
        public int PurchaseCount { get; set; }
        public decimal TotalSpent { get; set; }
        public bool NeedsFollowUp { get; set; }
    }

}
