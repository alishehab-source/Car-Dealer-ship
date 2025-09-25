namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveResponseDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string? CarImageUrl { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public int? DistanceCovered { get; set; }
        public int? MileageBefore { get; set; }
        public int? MileageAfter { get; set; }
        public int? FuelLevelBefore { get; set; }
        public int? FuelLevelAfter { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? CancellationReason { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime? RescheduleDate { get; set; }
        public string? RescheduleReason { get; set; }
        public string? StartLocation { get; set; }
        public string? PlannedRoute { get; set; }
        public string? ActualRoute { get; set; }
        public bool HasValidLicense { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string? LicenseIssuedBy { get; set; }
        public bool IsInsured { get; set; }
        public string? InsuranceCompany { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public DateTime? InsuranceExpiryDate { get; set; }
        public string? AccompanyingDriver { get; set; }
        public string? AccompanyingDriverPhone { get; set; }
        public string? AccompanyingDriverRelation { get; set; }
        public int? CustomerRating { get; set; }
        public int? ServiceRating { get; set; }
        public string? CustomerFeedback { get; set; }
        public string? EmployeeNotes { get; set; }
        public bool IsInterestedInPurchase { get; set; }
        public decimal? ProposedPrice { get; set; }
        public string? ProposedTerms { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string? FollowUpNotes { get; set; }
        public bool HasIncident { get; set; }
        public string? IncidentDetails { get; set; }
        public string? IncidentType { get; set; }
        public decimal? RepairCost { get; set; }
        public bool IsReported { get; set; }
        public string? ReportNumber { get; set; }
        public string? ReportedTo { get; set; }
        public string? WeatherCondition { get; set; }
        public string? RoadCondition { get; set; }
        public string? TrafficCondition { get; set; }
        public int? Temperature { get; set; }
        public bool RequiresFollowUp { get; set; }
        public string FollowUpPriority { get; set; } = string.Empty;
        public string? RequiredActions { get; set; }
        public decimal? FuelCost { get; set; }
        public decimal? MaintenanceCost { get; set; }
        public string? DriveType { get; set; }
        public bool IsConvertedToSale { get; set; }
        public int? SaleId { get; set; }
        public DateTime? ConversionDate { get; set; }
        public int? DaysToConversion { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Computed Properties
        public bool IsCompleted => Status == "Completed";
        public bool IsScheduled => Status == "Scheduled";
        public bool InProgress => Status == "InProgress";
        public bool IsCancelled => Status == "Cancelled";
        public int ActualDuration => ActualStartTime.HasValue && ActualEndTime.HasValue
            ? (int)(ActualEndTime.Value - ActualStartTime.Value).TotalMinutes : 0;
        public bool IsOverdue => Status == "Scheduled" && ScheduledDateTime < DateTime.Now;
        public bool NeedsFollowUp => RequiresFollowUp && !IsConvertedToSale && Status == "Completed";
        public string TimeUntilScheduled => ScheduledDateTime > DateTime.Now
            ? $"خلال {(ScheduledDateTime - DateTime.Now).TotalHours:F1} ساعة"
            : "منتهي الصلاحية";
    }


}
