namespace CarDealershipAPI.DTOs.TestDrive
{
    public class SaleDetailsDto
    {
        public int Id { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
    }

}
