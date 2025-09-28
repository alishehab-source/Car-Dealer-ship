namespace CarDealershipAPI.DTOs.Sale
{
    public class SaleDetailsDto : SaleDto
    {
        public string CarMake { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public int CarYear { get; set; }
        public string? CarColor { get; set; }
        public string? CarVIN { get; set; }

        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        public string EmployeeEmail { get; set; } = string.Empty;
        public string EmployeeDepartment { get; set; } = string.Empty;
    }


}
