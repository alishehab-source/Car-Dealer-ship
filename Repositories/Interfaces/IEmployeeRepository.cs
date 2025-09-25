using CarDealershipAPI.Models;

namespace CarDealershipAPI.Repositories.Interfaces
{

    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(string role);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetSalesRepresentativesAsync();
        Task<decimal> GetTotalCommissionAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<bool> IsEmailExistsAsync(string email);
    }

}
