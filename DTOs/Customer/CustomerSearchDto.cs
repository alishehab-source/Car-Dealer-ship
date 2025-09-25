namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerSearchDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public decimal? MinBudget { get; set; }
        public decimal? MaxBudget { get; set; }
        public string? PreferredMake { get; set; }
        public string? Source { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public DateTime? LastContactAfter { get; set; }
        public DateTime? LastContactBefore { get; set; }
        public bool? NeedsFollowUp { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public string? SearchTerm { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Sorting
        public string? SortBy { get; set; } = "CreatedDate";
        public bool SortDescending { get; set; } = true;
    }


}
