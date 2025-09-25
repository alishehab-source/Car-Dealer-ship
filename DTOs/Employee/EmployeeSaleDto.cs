namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeSaleDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public decimal? Commission { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
