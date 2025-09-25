namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string IdentityNumber { get; set; } = string.Empty;
        public string IdentityType { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal? MaxCommission { get; set; }
        public decimal? Bonus { get; set; }
        public int AnnualLeave { get; set; }
        public int UsedLeave { get; set; }
        public int SickLeave { get; set; }
        public int UsedSickLeave { get; set; }
        public string Status { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public int? SupervisorId { get; set; }
        public string? SupervisorName { get; set; }
        public string? Education { get; set; }
        public string? Skills { get; set; }
        public string? Certifications { get; set; }
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? Notes { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalSalesValue { get; set; }
        public decimal TotalCommissionEarned { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public decimal? PerformanceRating { get; set; }

        // Computed Properties
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public int YearsOfService => DateTime.Now.Year - HireDate.Year;
        public int RemainingLeave => AnnualLeave - UsedLeave;
        public int RemainingSickLeave => SickLeave - UsedSickLeave;
        public bool IsActive => Status == "Active";
        public string FullAddress => string.Join(", ", new[] { Address, City, State }.Where(s => !string.IsNullOrEmpty(s)));
        public decimal AverageMonthlySales => TotalSales > 0 && YearsOfService > 0 ? TotalSalesValue / (YearsOfService * 12) : 0;
    }
}
