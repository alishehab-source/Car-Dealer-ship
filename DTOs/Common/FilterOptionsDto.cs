namespace CarDealershipAPI.DTOs.common
{
    public class FilterOptionsDto
    {
        public List<string> Makes { get; set; } = new List<string>();
        public List<string> Models { get; set; } = new List<string>();
        public List<string> Colors { get; set; } = new List<string>();
        public List<string> FuelTypes { get; set; } = new List<string>();
        public List<string> Transmissions { get; set; } = new List<string>();
        public List<string> Conditions { get; set; } = new List<string>();
        public List<string> Statuses { get; set; } = new List<string>();
        public List<string> PaymentMethods { get; set; } = new List<string>();
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> Departments { get; set; } = new List<string>();
        public PriceRange PriceRange { get; set; } = new PriceRange();
        public YearRange YearRange { get; set; } = new YearRange();
    }
}
