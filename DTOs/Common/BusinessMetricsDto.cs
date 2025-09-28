namespace CarDealershipAPI.DTOs.common
{
    public class BusinessMetricsDto
    {
        public decimal TotalRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal RevenueGrowth { get; set; }
        public int TotalSales { get; set; }
        public int MonthlySales { get; set; }
        public decimal SalesGrowth { get; set; }
        public decimal AverageOrderValue { get; set; }
        public decimal CustomerLifetimeValue { get; set; }
        public int ActiveCustomers { get; set; }
        public int NewCustomers { get; set; }
        public decimal CustomerRetentionRate { get; set; }
        public decimal InventoryTurnover { get; set; }
        public int DaysInInventory { get; set; }
        public decimal GrossMargin { get; set; }
        public decimal NetProfitMargin { get; set; }
    }

}
