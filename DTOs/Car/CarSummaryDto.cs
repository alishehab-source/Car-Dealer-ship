namespace CarDealershipAPI.DTOs.Car
{
    public class CarSummaryDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty;
        public string DisplayName => $"{Make} {Model} {Year}";
    }

}
