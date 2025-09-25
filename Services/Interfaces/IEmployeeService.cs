using CarDealershipAPI.DTOs.Employee;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<EmployeeResponseDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> TerminateEmployeeAsync(int employeeId, DateTime terminationDate, string reason);

        Task<IEnumerable<EmployeeListDto>> SearchEmployeesAsync(EmployeeSearchDto searchDto);
        Task<EmployeeDetailsDto?> GetEmployeeDetailsAsync(int id);
        Task<IEnumerable<EmployeeListDto>> GetActiveEmployeesAsync();
        Task<IEnumerable<EmployeeListDto>> GetEmployeesByRoleAsync(string role);
        Task<IEnumerable<EmployeeListDto>> GetSalesRepresentativesAsync();

        Task<bool> IsEmailUniqueAsync(string email, int? excludeEmployeeId = null);
        Task<bool> IsIdentityNumberUniqueAsync(string identityNumber, int? excludeEmployeeId = null);
        Task<bool> ValidateEmployeeDataAsync(CreateEmployeeDto dto);
        Task<bool> CanEmployeeAccessCustomerAsync(int employeeId, int customerId);

        Task<EmployeeStatisticsDto> GetEmployeePerformanceAsync(int employeeId);
        Task<bool> UpdateEmployeePerformanceAsync(EmployeePerformanceUpdateDto dto);
        Task<decimal> CalculateEmployeeCommissionAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<decimal> GetEmployeeSalesTargetAsync(int employeeId);
        Task<decimal> CalculateTargetAchievementAsync(int employeeId);

        Task<IEnumerable<EmployeeSubordinateDto>> GetEmployeeSubordinatesAsync(int supervisorId);
        Task<bool> AssignSupervisorAsync(int employeeId, int supervisorId);
        Task<EmployeeResponseDto?> GetEmployeeSupervisorAsync(int employeeId);

        Task<IEnumerable<EmployeeSummaryDto>> GetTopPerformingEmployeesAsync(int count = 10);
        Task<EmployeeStatisticsDto> GetTeamStatisticsAsync(int supervisorId);
        Task<decimal> GetTotalPayrollAsync();
        Task<int> GetTotalActiveEmployeesCountAsync();

        Task<bool> RequestLeaveAsync(int employeeId, DateTime startDate, DateTime endDate, string leaveType, string reason);
        Task<bool> ApproveLeaveAsync(int leaveRequestId, int approvedBy);
        Task<IEnumerable<EmployeeLeaveDto>> GetEmployeeLeavesAsync(int employeeId);
        Task<EmployeeScheduleDto> GetEmployeeScheduleAsync(int employeeId);
    }

}
