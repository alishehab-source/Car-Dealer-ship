namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerDetailsDto : CustomerResponseDto
    {
        public List<CustomerSaleDto>? PurchaseHistory { get; set; }
        public List<CustomerTestDriveDto>? TestDriveHistory { get; set; }
        public List<CustomerInteractionDto>? InteractionHistory { get; set; }
        public CustomerStatisticsDto? Statistics { get; set; }
        public List<CarRecommendationDto>? RecommendedCars { get; set; }
    }

}
