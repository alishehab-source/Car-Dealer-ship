using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Customer;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<PagedResult<CustomerDto>> GetAllCustomersAsync(CustomerSearchDto searchDto);
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(int id);
        Task<CustomerWithSalesDto?> GetCustomerWithSalesAsync(int id);
        Task<List<CustomerSummaryDto>> GetTopCustomersAsync(int count = 10);
        Task<bool> IsEmailExistsAsync(string email, int? excludeId = null);
        Task<bool> IsIdentityNumberExistsAsync(string identityNumber, int? excludeId = null);
    }
}
