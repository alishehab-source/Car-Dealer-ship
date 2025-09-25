namespace CarDealershipAPI.DTO.Car
{
    public class SaleHistoryDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
