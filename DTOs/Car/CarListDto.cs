namespace CarDealershipAPI.DTO.Car
{
    public class CarListDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? MainImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        // Quick stats
        public bool IsAvailable => Status == "Available";
        public string DisplayName => $"{Year} {Make} {Model}";
        public int DaysOnLot => (DateTime.Now - CreatedDate).Days;
    }

}
