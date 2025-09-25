using CarDealershipAPI.DTOs.Customer;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);
        Task<CustomerResponseDto> UpdateCustomerAsync(UpdateCustomerDto dto);
        Task<bool> DeleteCustomerAsync(int id);

        Task<IEnumerable<CustomerListDto>> SearchCustomersAsync(CustomerSearchDto searchDto);
        Task<CustomerDetailsDto?> GetCustomerDetailsAsync(int id);
        Task<bool> RecordCustomerContactAsync(CustomerContactDto dto);
        Task<bool> UpdateCustomerStatusAsync(int customerId, string status);
        Task<bool> ScheduleFollowUpAsync(int customerId, DateTime followUpDate, string notes);

        Task<bool> IsPhoneNumberUniqueAsync(string phone, int? excludeCustomerId = null);
        Task<bool> IsEmailUniqueAsync(string email, int? excludeCustomerId = null);
        Task<bool> ValidateCustomerDataAsync(CreateCustomerDto dto);
        Task<CustomerStatisticsDto> CalculateCustomerScoreAsync(int customerId);
        Task<string> DetermineCustomerSegmentAsync(int customerId);

        Task<IEnumerable<CustomerListDto>> GetHotLeadsAsync();
        Task<IEnumerable<CustomerListDto>> GetCustomersNeedingFollowUpAsync();
        Task<IEnumerable<CustomerListDto>> GetLostCustomersAsync(int daysThreshold = 180);
        Task<bool> ConvertLeadToCustomerAsync(int customerId);
        Task<decimal> CalculateLeadConversionRateAsync();

        Task<CustomerStatisticsDto> GetCustomerStatisticsAsync(int customerId);
        Task<IEnumerable<CustomerSummaryDto>> GetTopCustomersAsync(int count = 10);
        Task<decimal> GetCustomerLifetimeValueAsync(int customerId);
        Task<int> GetTotalCustomersCountAsync();
        Task<decimal> GetAverageCustomerBudgetAsync();

        Task<IEnumerable<CarRecommendationDto>> GetRecommendedCarsAsync(int customerId);
        Task<IEnumerable<CustomerListDto>> FindMatchingCustomersForCarAsync(int carId);
        Task<int> CalculateCarMatchScoreAsync(int customerId, int carId);
    }

}
