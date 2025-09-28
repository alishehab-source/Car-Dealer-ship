namespace CarDealershipAPI.DTOs.common
{
    public class MonthlySalesChartDto
    {
        public string Month { get; set; } = string.Empty;
        public int Year { get; set; }
        public int SalesCount { get; set; }
        public decimal Revenue { get; set; }
    }

}
