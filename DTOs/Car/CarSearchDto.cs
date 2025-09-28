namespace CarDealershipAPI.DTOs.Car
{
    public class CarSearchDto
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Color { get; set; }
        public int? MaxMileage { get; set; }
        public string? FuelType { get; set; }
        public string? Transmission { get; set; }
        public string? Condition { get; set; }
        public string? Status { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; } = "CreatedDate";
        public bool SortDescending { get; set; } = true;
    }

}
