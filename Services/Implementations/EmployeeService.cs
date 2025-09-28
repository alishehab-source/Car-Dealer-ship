using CarDealershipAPI.Data;
using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Employee;
using CarDealershipAPI.Models;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CarDealershipDbContext _context;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(CarDealershipDbContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedResult<EmployeeDto>> GetAllEmployeesAsync(EmployeeSearchDto searchDto)
        {
            var query = _context.Employees.AsQueryable();


            if (!string.IsNullOrEmpty(searchDto.Name))
                query = query.Where(e => e.Name.Contains(searchDto.Name));

            if (!string.IsNullOrEmpty(searchDto.Email))
                query = query.Where(e => e.Email.Contains(searchDto.Email));

            if (!string.IsNullOrEmpty(searchDto.Role))
                query = query.Where(e => e.Role == searchDto.Role);

            if (!string.IsNullOrEmpty(searchDto.Department))
                query = query.Where(e => e.Department == searchDto.Department);

            if (!string.IsNullOrEmpty(searchDto.Status))
                query = query.Where(e => e.Status == searchDto.Status);

            if (searchDto.SupervisorId.HasValue)
                query = query.Where(e => e.SupervisorId == searchDto.SupervisorId.Value);

            if (searchDto.HiredAfter.HasValue)
                query = query.Where(e => e.HireDate >= searchDto.HiredAfter.Value);

            if (searchDto.HiredBefore.HasValue)
                query = query.Where(e => e.HireDate <= searchDto.HiredBefore.Value);

            if (searchDto.MinSalary.HasValue)
                query = query.Where(e => e.BaseSalary >= searchDto.MinSalary.Value);

            if (searchDto.MaxSalary.HasValue)
                query = query.Where(e => e.BaseSalary <= searchDto.MaxSalary.Value);

            if (searchDto.MinTotalSales.HasValue)
                query = query.Where(e => e.TotalSales >= searchDto.MinTotalSales.Value);

            if (searchDto.MinTotalSalesValue.HasValue)
                query = query.Where(e => e.TotalSalesValue >= searchDto.MinTotalSalesValue.Value);

            if (searchDto.MinYearsOfService.HasValue || searchDto.MaxYearsOfService.HasValue)
            {
                var today = DateTime.Today;
                if (searchDto.MinYearsOfService.HasValue)
                {
                    var maxHireDate = today.AddYears(-searchDto.MinYearsOfService.Value);
                    query = query.Where(e => e.HireDate <= maxHireDate);
                }
                if (searchDto.MaxYearsOfService.HasValue)
                {
                    var minHireDate = today.AddYears(-searchDto.MaxYearsOfService.Value);
                    query = query.Where(e => e.HireDate >= minHireDate);
                }
            }


            query = ApplySorting(query, searchDto.SortBy, searchDto.SortDescending);


            var totalRecords = await query.CountAsync();


            var employees = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Include(e => e.Supervisor)
                .Include(e => e.Subordinates)
                .Include(e => e.Sales)
                .ToListAsync();

            var employeeDtos = employees.Select(MapToEmployeeDto).ToList();

            return new PagedResult<EmployeeDto>(employeeDtos, totalRecords, searchDto.Page, searchDto.PageSize);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Supervisor)
                .Include(e => e.Subordinates)
                .Include(e => e.Sales)
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee != null ? MapToEmployeeDto(employee) : null;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                Name = createEmployeeDto.Name,
                Email = createEmployeeDto.Email,
                Phone = createEmployeeDto.Phone,
                IdentityNumber = createEmployeeDto.IdentityNumber,
                HireDate = createEmployeeDto.HireDate,
                Role = createEmployeeDto.Role,
                Department = createEmployeeDto.Department,
                BaseSalary = createEmployeeDto.BaseSalary,
                CommissionRate = createEmployeeDto.CommissionRate,
                Status = createEmployeeDto.Status,
                SupervisorId = createEmployeeDto.SupervisorId,
                CreatedDate = DateTime.Now
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Employee created successfully with ID: {EmployeeId}", employee.Id);

            return MapToEmployeeDto(employee);
        }

        public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return null;

            if (!string.IsNullOrEmpty(updateEmployeeDto.Name))
                employee.Name = updateEmployeeDto.Name;

            if (!string.IsNullOrEmpty(updateEmployeeDto.Email))
                employee.Email = updateEmployeeDto.Email;

            if (!string.IsNullOrEmpty(updateEmployeeDto.Phone))
                employee.Phone = updateEmployeeDto.Phone;

            if (!string.IsNullOrEmpty(updateEmployeeDto.IdentityNumber))
                employee.IdentityNumber = updateEmployeeDto.IdentityNumber;

            if (updateEmployeeDto.HireDate.HasValue)
                employee.HireDate = updateEmployeeDto.HireDate.Value;

            if (!string.IsNullOrEmpty(updateEmployeeDto.Role))
                employee.Role = updateEmployeeDto.Role;

            if (!string.IsNullOrEmpty(updateEmployeeDto.Department))
                employee.Department = updateEmployeeDto.Department;

            if (updateEmployeeDto.BaseSalary.HasValue)
                employee.BaseSalary = updateEmployeeDto.BaseSalary.Value;

            if (updateEmployeeDto.CommissionRate.HasValue)
                employee.CommissionRate = updateEmployeeDto.CommissionRate.Value;

            if (!string.IsNullOrEmpty(updateEmployeeDto.Status))
                employee.Status = updateEmployeeDto.Status;

            if (updateEmployeeDto.SupervisorId.HasValue)
                employee.SupervisorId = updateEmployeeDto.SupervisorId.Value;

            employee.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Employee updated successfully with ID: {EmployeeId}", employee.Id);

            return MapToEmployeeDto(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return false;


            var hasSales = await _context.Sales.AnyAsync(s => s.EmployeeId == id);
            if (hasSales)
            {
                _logger.LogWarning("Cannot delete employee with ID: {EmployeeId} because they have sales records", id);
                return false;
            }


            var hasSubordinates = await _context.Employees.AnyAsync(e => e.SupervisorId == id);
            if (hasSubordinates)
            {
                _logger.LogWarning("Cannot delete employee with ID: {EmployeeId} because they have subordinates", id);
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Employee deleted successfully with ID: {EmployeeId}", id);

            return true;
        }

        public async Task<EmployeeWithSalesDto?> GetEmployeeWithSalesAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Supervisor)
                .Include(e => e.Subordinates)
                .Include(e => e.Sales)
                    .ThenInclude(s => s.Car)
                .Include(e => e.Sales)
                    .ThenInclude(s => s.Customer)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return null;

            var employeeDto = MapToEmployeeDto(employee);
            var recentSales = employee.Sales
                .OrderByDescending(s => s.SaleDate)
                .Take(10)
                .Select(s => new EmployeeSaleDto
                {
                    SaleId = s.Id,
                    CustomerName = s.Customer.Name,
                    CarInfo = $"{s.Car.Make} {s.Car.Model} {s.Car.Year}",
                    SalePrice = s.SalePrice,
                    Commission = s.EmployeeCommission ?? 0,
                    SaleDate = s.SaleDate,
                    Status = s.Status
                }).ToList();

            return new EmployeeWithSalesDto
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                IdentityNumber = employeeDto.IdentityNumber,
                HireDate = employeeDto.HireDate,
                Role = employeeDto.Role,
                Department = employeeDto.Department,
                BaseSalary = employeeDto.BaseSalary,
                CommissionRate = employeeDto.CommissionRate,
                Status = employeeDto.Status,
                SupervisorId = employeeDto.SupervisorId,
                SupervisorName = employeeDto.SupervisorName,
                TotalSales = employeeDto.TotalSales,
                TotalSalesValue = employeeDto.TotalSalesValue,
                TotalCommissionEarned = employeeDto.TotalCommissionEarned,
                LastSaleDate = employeeDto.LastSaleDate,
                CreatedDate = employeeDto.CreatedDate,
                UpdatedDate = employeeDto.UpdatedDate,
                SubordinatesCount = employeeDto.SubordinatesCount,
                RecentSales = recentSales
            };
        }

        public async Task<List<EmployeePerformanceDto>> GetTopPerformersAsync(int count = 10)
        {
            var topPerformers = await _context.Employees
                .Where(e => e.Status == "Active")
                .Include(e => e.Sales)
                .Select(e => new EmployeePerformanceDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Role = e.Role,
                    Department = e.Department,
                    TotalSales = e.TotalSales,
                    TotalSalesValue = e.TotalSalesValue,
                    TotalCommissionEarned = e.TotalCommissionEarned,
                    AverageSaleValue = e.TotalSales > 0 ? e.TotalSalesValue / e.TotalSales : 0,
                    LastSaleDate = e.LastSaleDate,
                    SalesThisMonth = e.Sales.Count(s => s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year),
                    SalesValueThisMonth = e.Sales.Where(s => s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year).Sum(s => s.SalePrice),
                    CommissionThisMonth = e.Sales.Where(s => s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year).Sum(s => s.EmployeeCommission ?? 0)
                })
                .OrderByDescending(e => e.TotalSalesValue)
                .Take(count)
                .ToListAsync();

            return topPerformers;
        }

        public async Task<EmployeeHierarchyDto?> GetEmployeeHierarchyAsync(int supervisorId)
        {
            var supervisor = await _context.Employees
                .Include(e => e.Subordinates)
                .FirstOrDefaultAsync(e => e.Id == supervisorId);

            if (supervisor == null)
                return null;

            return await BuildHierarchy(supervisor);
        }

        public async Task<List<EmployeeSummaryDto>> GetActiveEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => e.Status == "Active")
                .Select(e => new EmployeeSummaryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Role = e.Role,
                    Department = e.Department,
                    Status = e.Status,
                    TotalSales = e.TotalSales,
                    TotalSalesValue = e.TotalSalesValue
                })
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        public async Task<bool> UpdateEmployeePerformanceAsync(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Sales)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return false;

            var completedSales = employee.Sales.Where(s => s.Status == "Completed");

            employee.TotalSales = completedSales.Count();
            employee.TotalSalesValue = completedSales.Sum(s => s.SalePrice);
            employee.TotalCommissionEarned = completedSales.Sum(s => s.EmployeeCommission ?? 0);
            employee.LastSaleDate = completedSales.Any() ? completedSales.Max(s => s.SaleDate) : null;

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<EmployeeHierarchyDto> BuildHierarchy(Employee employee)
        {
            var subordinates = new List<EmployeeHierarchyDto>();

            foreach (var subordinate in employee.Subordinates)
            {
                subordinates.Add(await BuildHierarchy(subordinate));
            }

            return new EmployeeHierarchyDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Role = employee.Role,
                Department = employee.Department,
                Subordinates = subordinates
            };
        }

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, string? sortBy, bool sortDescending)
        {
            return sortBy?.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(e => e.Name) : query.OrderBy(e => e.Name),
                "email" => sortDescending ? query.OrderByDescending(e => e.Email) : query.OrderBy(e => e.Email),
                "role" => sortDescending ? query.OrderByDescending(e => e.Role) : query.OrderBy(e => e.Role),
                "department" => sortDescending ? query.OrderByDescending(e => e.Department) : query.OrderBy(e => e.Department),
                "hiredate" => sortDescending ? query.OrderByDescending(e => e.HireDate) : query.OrderBy(e => e.HireDate),
                "basesalary" => sortDescending ? query.OrderByDescending(e => e.BaseSalary) : query.OrderBy(e => e.BaseSalary),
                "totalsales" => sortDescending ? query.OrderByDescending(e => e.TotalSales) : query.OrderBy(e => e.TotalSales),
                "totalsalesvalue" => sortDescending ? query.OrderByDescending(e => e.TotalSalesValue) : query.OrderBy(e => e.TotalSalesValue),
                "createddate" => sortDescending ? query.OrderByDescending(e => e.CreatedDate) : query.OrderBy(e => e.CreatedDate),
                _ => sortDescending ? query.OrderByDescending(e => e.CreatedDate) : query.OrderBy(e => e.CreatedDate)
            };
        }

        private EmployeeDto MapToEmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                IdentityNumber = employee.IdentityNumber,
                HireDate = employee.HireDate,
                Role = employee.Role,
                Department = employee.Department,
                BaseSalary = employee.BaseSalary,
                CommissionRate = employee.CommissionRate,
                Status = employee.Status,
                SupervisorId = employee.SupervisorId,
                SupervisorName = employee.Supervisor?.Name,
                TotalSales = employee.TotalSales,
                TotalSalesValue = employee.TotalSalesValue,
                TotalCommissionEarned = employee.TotalCommissionEarned,
                LastSaleDate = employee.LastSaleDate,
                CreatedDate = employee.CreatedDate,
                UpdatedDate = employee.UpdatedDate,
                SubordinatesCount = employee.Subordinates?.Count ?? 0
            };
        }

    }
}