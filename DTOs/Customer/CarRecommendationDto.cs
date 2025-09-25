namespace CarDealershipAPI.DTOs.Customer
{

    public class CarRecommendationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string RecommendationReason { get; set; } = string.Empty;
        public int MatchScore { get; set; } 
    }

}
