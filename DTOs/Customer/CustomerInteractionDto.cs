namespace CarDealershipAPI.DTOs.Customer
{

    public class CustomerInteractionDto
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string? Outcome { get; set; }
    }

}
