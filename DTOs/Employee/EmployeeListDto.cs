namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalSalesValue { get; set; }
        public decimal? PerformanceRating { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public string? ProfileImageUrl { get; set; }

        // Quick indicators
        public bool IsActive => Status == "Active";
        public int YearsOfService => DateTime.Now.Year - HireDate.Year;
        public string StatusColor => Status switch
        {
            "Active" => "#00aa00",
            "OnLeave" => "#ff8800",
            "Inactive" => "#888888",
            "Terminated" => "#ff4444",
            "Suspended" => "#cc0000",
            _ => "#666666"
        };
        public string PerformanceLevel => PerformanceRating switch
        {
            >= 4.5m => "ممتاز",
            >= 3.5m => "جيد جداً",
            >= 2.5m => "جيد",
            >= 1.5m => "مقبول",
            _ => "ضعيف"
        };
    }

}
