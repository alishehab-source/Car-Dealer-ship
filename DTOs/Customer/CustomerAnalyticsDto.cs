namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerAnalyticsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
        public int TotalPurchases { get; set; }
        public decimal AverageOrderValue { get; set; }
        public DateTime? FirstPurchaseDate { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public string CustomerSegment { get; set; } = string.Empty; 
        public List<string> PreferredBrands { get; set; } = new List<string>();
    }

}
