using CarDealershipAPI.Data;
using CarDealershipAPI.Models;
using CarDealershipAPI.Repositories.Interfaces;

namespace CarDealershipAPI.Repositories.Implementations
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CarDealershipContext context, ILogger<Repository<Customer>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Customer>> GetHotLeadsAsync()
        {
            return await GetAllAsync(c => c.Status == "Hot");
        }

        public async Task<IEnumerable<Customer>> GetCustomersByStatusAsync(string status)
        {
            return await GetAllAsync(c => c.Status == status);
        }

        public async Task<Customer?> GetCustomerByPhoneAsync(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return null;
            return await GetFirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            return await GetFirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByBudgetRangeAsync(decimal minBudget, decimal maxBudget)
        {
            return await GetAllAsync(c => c.Budget >= minBudget && c.Budget <= maxBudget);
        }

        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return await GetAllAsync();

            return await GetAllAsync(c =>
                c.Name.Contains(searchTerm) ||
                c.Phone.Contains(searchTerm) ||
                c.Email!.Contains(searchTerm) ||
                c.Address!.Contains(searchTerm));
        }

        public async Task UpdateLastContactDateAsync(int customerId)
        {
            var customer = await GetByIdAsync(customerId);
            if (customer != null)
            {
                customer.LastContactDate = DateTime.Now;
                customer.UpdatedDate = DateTime.Now;
                Update(customer);
            }
        }

        public async Task<bool> IsPhoneExistsAsync(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return await ExistsAsync(c => c.Phone == phone);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return await ExistsAsync(c => c.Email == email);
        }
    }
}
