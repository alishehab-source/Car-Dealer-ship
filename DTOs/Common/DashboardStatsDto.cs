namespace CarDealershipAPI.DTOs.common
{
    public class DashboardStatsDto
    {
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int SoldCars { get; set; }
        public int ReservedCars { get; set; }

        public int TotalCustomers { get; set; }
        public int NewCustomersThisMonth { get; set; }

        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }

        public int TotalSalesToday { get; set; }
        public int TotalSalesThisMonth { get; set; }
        public int TotalSalesThisYear { get; set; }

        public decimal RevenueToday { get; set; }
        public decimal RevenueThisMonth { get; set; }
        public decimal RevenueThisYear { get; set; }

        public decimal AverageSaleValue { get; set; }
        public decimal TotalCommissionsThisMonth { get; set; }


        public List<MonthlySalesChartDto> MonthlySalesChart { get; set; } = new List<MonthlySalesChartDto>();
        public List<TopSellingBrandDto> TopSellingBrands { get; set; } = new List<TopSellingBrandDto>();
        public List<EmployeePerformanceChartDto> TopPerformers { get; set; } = new List<EmployeePerformanceChartDto>();
    }

}
