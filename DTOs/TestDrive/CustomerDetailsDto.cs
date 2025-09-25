namespace CarDealershipAPI.DTOs.TestDrive
{
    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TotalTestDrives { get; set; }
        public int TotalPurchases { get; set; }
    }
   
}
