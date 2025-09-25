namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerSaleDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
    }

}
