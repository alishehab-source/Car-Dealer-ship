namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeSaleDto
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CarInfo { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public decimal Commission { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
