namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int TotalPurchases { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
