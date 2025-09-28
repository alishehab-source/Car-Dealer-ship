namespace CarDealershipAPI.DTOs.Sale
{
    public class SaleSummaryDto
    {
        public int Id { get; set; }
        public string CarInfo { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
