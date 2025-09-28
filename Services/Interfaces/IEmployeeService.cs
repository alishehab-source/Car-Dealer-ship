using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Employee;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeDto>> GetAllEmployeesAsync(EmployeeSearchDto searchDto);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<EmployeeWithSalesDto?> GetEmployeeWithSalesAsync(int id);
        Task<List<EmployeePerformanceDto>> GetTopPerformersAsync(int count = 10);
        Task<EmployeeHierarchyDto?> GetEmployeeHierarchyAsync(int supervisorId);
        Task<List<EmployeeSummaryDto>> GetActiveEmployeesAsync();
        Task<bool> UpdateEmployeePerformanceAsync(int employeeId);
    }
}
