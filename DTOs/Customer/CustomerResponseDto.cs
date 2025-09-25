namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Occupation { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public decimal Budget { get; set; }
        public string? PreferredMake { get; set; }
        public string? PreferredModel { get; set; }
        public int? PreferredYear { get; set; }
        public string? PreferredColor { get; set; }
        public string? PreferredFuelType { get; set; }
        public string? PreferredTransmission { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Source { get; set; }
        public string? SourceDetails { get; set; }
        public DateTime? LastContactDate { get; set; }
        public DateTime? NextContactDate { get; set; }
        public string? Notes { get; set; }
        public int? Rating { get; set; }
        public bool HasDriverLicense { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public DateTime? DriverLicenseExpiryDate { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public bool OptInForMarketing { get; set; }
        public bool OptInForSMS { get; set; }
        public bool OptInForEmail { get; set; }
        public string PreferredLanguage { get; set; } = string.Empty;
        public string PreferredContactMethod { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public int TotalPurchases { get; set; }
        public decimal TotalPurchaseValue { get; set; }

        // Computed Properties
        public int Age => DateOfBirth.HasValue ? DateTime.Now.Year - DateOfBirth.Value.Year : 0;
        public string FullAddress => string.Join(", ", new[] { Address, City, State }.Where(s => !string.IsNullOrEmpty(s)));
        public bool IsHotLead => Status == "Hot";
        public int DaysSinceLastContact => LastContactDate.HasValue ? (DateTime.Now - LastContactDate.Value).Days : 0;
        public bool NeedsFollowUp => NextContactDate.HasValue && NextContactDate.Value <= DateTime.Now;
    }

}
