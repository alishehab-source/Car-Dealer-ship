namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerSaleDto
    {
        public int SaleId { get; set; }
        public string CarInfo { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
    }

}
