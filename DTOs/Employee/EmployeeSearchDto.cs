namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeSearchDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Department { get; set; }
        public string? Status { get; set; }
        public int? SupervisorId { get; set; }
        public DateTime? HiredAfter { get; set; }
        public DateTime? HiredBefore { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public int? MinYearsOfService { get; set; }
        public int? MaxYearsOfService { get; set; }

        public int? MinTotalSales { get; set; }
        public decimal? MinTotalSalesValue { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; } = "CreatedDate";
        public bool SortDescending { get; set; } = true;
    }

}
