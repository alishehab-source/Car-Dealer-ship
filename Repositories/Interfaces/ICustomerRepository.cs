using CarDealershipAPI.Models;

namespace CarDealershipAPI.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetHotLeadsAsync();
        Task<IEnumerable<Customer>> GetCustomersByStatusAsync(string status);
        Task<Customer?> GetCustomerByPhoneAsync(string phone);
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<IEnumerable<Customer>> GetCustomersByBudgetRangeAsync(decimal minBudget, decimal maxBudget);
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
        Task UpdateLastContactDateAsync(int customerId);
        Task<bool> IsPhoneExistsAsync(string phone);
        Task<bool> IsEmailExistsAsync(string email);
    }
}
