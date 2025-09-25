namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveSearchDto
    {
        public string? CustomerName { get; set; }
        public string? EmployeeName { get; set; }
        public string? CarMake { get; set; }
        public string? CarModel { get; set; }
        public string? Status { get; set; }
        public DateTime? ScheduledFrom { get; set; }
        public DateTime? ScheduledTo { get; set; }
        public bool? IsInterestedInPurchase { get; set; }
        public bool? IsConvertedToSale { get; set; }
        public bool? HasIncident { get; set; }
        public bool? RequiresFollowUp { get; set; }
        public string? FollowUpPriority { get; set; }
        public int? MinCustomerRating { get; set; }
        public int? MaxCustomerRating { get; set; }
        public string? WeatherCondition { get; set; }
        public string? DriveType { get; set; }
        public string? SearchTerm { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; } = "ScheduledDateTime";
        public bool SortDescending { get; set; } = true;
    }

}
