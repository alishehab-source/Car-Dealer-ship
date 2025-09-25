namespace CarDealershipAPI.DTO.Car
{
    public class CarResponseDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string? VIN { get; set; }
        public string? Color { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public double? EngineSize { get; set; }
        public int? Horsepower { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int? Doors { get; set; }
        public int? Seats { get; set; }
        public string? DriveType { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public string? Notes { get; set; }
        public string? MainImageUrl { get; set; }
        public List<string>? ImageUrls { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? Source { get; set; }
        public bool IsFinanced { get; set; }
        public string? FinancingCompany { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public int? PreviousOwners { get; set; }
        public bool HasAccidents { get; set; }
        public string? AccidentHistory { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? SoldDate { get; set; }

        // Statistics
        public int TotalSales { get; set; }
        public int TotalTestDrives { get; set; }
    }

}
