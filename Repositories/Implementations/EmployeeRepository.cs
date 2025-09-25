using CarDealershipAPI.Data;
using CarDealershipAPI.Models;
using CarDealershipAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Repositories.Implementations
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CarDealershipContext context, ILogger<Repository<Employee>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        {
            return await GetAllAsync(e => e.Status == "Active");
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(string role)
        {
            return await GetAllAsync(e => e.Role == role);
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            return await GetFirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetSalesRepresentativesAsync()
        {
            return await GetAllAsync(e => e.Role == "SalesRep" && e.Status == "Active");
        }

        public async Task<decimal> GetTotalCommissionAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            var sales = await _context.Sales
                .Where(s => s.EmployeeId == employeeId &&
                           s.SaleDate >= startDate &&
                           s.SaleDate <= endDate &&
                           s.Status == "Completed")
                .Include(s => s.Employee)
                .ToListAsync();

            return sales.Sum(s => s.SalePrice * s.Employee.CommissionRate);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return await ExistsAsync(e => e.Email == email);
        }
    }
}
