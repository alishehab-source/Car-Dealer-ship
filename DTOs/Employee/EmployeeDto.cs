namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public decimal CommissionRate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? SupervisorId { get; set; }
        public string? SupervisorName { get; set; }

        public int TotalSales { get; set; }
        public decimal TotalSalesValue { get; set; }
        public decimal TotalCommissionEarned { get; set; }
        public DateTime? LastSaleDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int YearsOfService => (DateTime.Now - HireDate).Days / 365;
        public decimal AverageSaleValue => TotalSales > 0 ? TotalSalesValue / TotalSales : 0;
        public int SubordinatesCount { get; set; }
    }

}
