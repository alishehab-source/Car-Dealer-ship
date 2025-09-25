namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeSearchDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Department { get; set; }
        public string? Status { get; set; }
        public string? EmploymentType { get; set; }
        public DateTime? HiredAfter { get; set; }
        public DateTime? HiredBefore { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public decimal? MinPerformanceRating { get; set; }
        public decimal? MaxPerformanceRating { get; set; }
        public int? SupervisorId { get; set; }
        public string? SearchTerm { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Sorting
        public string? SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;
    }

}
