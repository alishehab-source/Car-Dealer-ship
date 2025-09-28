using CarDealershipAPI.Data;
using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Customer;
using CarDealershipAPI.Models;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly CarDealershipDbContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(CarDealershipDbContext context, ILogger<CustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedResult<CustomerDto>> GetAllCustomersAsync(CustomerSearchDto searchDto)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Name))
                query = query.Where(c => c.Name.Contains(searchDto.Name));

            if (!string.IsNullOrEmpty(searchDto.Email))
                query = query.Where(c => c.Email.Contains(searchDto.Email));

            if (!string.IsNullOrEmpty(searchDto.Phone))
                query = query.Where(c => c.Phone.Contains(searchDto.Phone));

            if (!string.IsNullOrEmpty(searchDto.IdentityNumber))
                query = query.Where(c => c.IdentityNumber.Contains(searchDto.IdentityNumber));

            if (!string.IsNullOrEmpty(searchDto.IdentityType))
                query = query.Where(c => c.IdentityType == searchDto.IdentityType);

            if (!string.IsNullOrEmpty(searchDto.Gender))
                query = query.Where(c => c.Gender == searchDto.Gender);

            if (searchDto.RegisteredAfter.HasValue)
                query = query.Where(c => c.CreatedDate >= searchDto.RegisteredAfter.Value);

            if (searchDto.RegisteredBefore.HasValue)
                query = query.Where(c => c.CreatedDate <= searchDto.RegisteredBefore.Value);

            if (searchDto.HasPurchases.HasValue)
            {
                if (searchDto.HasPurchases.Value)
                    query = query.Where(c => c.Sales.Any());
                else
                    query = query.Where(c => !c.Sales.Any());
            }


            if (searchDto.MinAge.HasValue || searchDto.MaxAge.HasValue)
            {
                var today = DateTime.Today;
                if (searchDto.MinAge.HasValue)
                {
                    var maxBirthDate = today.AddYears(-searchDto.MinAge.Value);
                    query = query.Where(c => c.DateOfBirth.HasValue && c.DateOfBirth.Value <= maxBirthDate);
                }
                if (searchDto.MaxAge.HasValue)
                {
                    var minBirthDate = today.AddYears(-searchDto.MaxAge.Value - 1);
                    query = query.Where(c => c.DateOfBirth.HasValue && c.DateOfBirth.Value > minBirthDate);
                }
            }


            query = ApplySorting(query, searchDto.SortBy, searchDto.SortDescending);


            var totalRecords = await query.CountAsync();

            var customers = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Include(c => c.Sales)
                .ToListAsync();

            var customerDtos = customers.Select(MapToCustomerDto).ToList();

            return new PagedResult<CustomerDto>(customerDtos, totalRecords, searchDto.Page, searchDto.PageSize);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Sales)
                .FirstOrDefaultAsync(c => c.Id == id);

            return customer != null ? MapToCustomerDto(customer) : null;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = new Customer
            {
                Name = createCustomerDto.Name,
                Email = createCustomerDto.Email,
                Phone = createCustomerDto.Phone,
                Address = createCustomerDto.Address,
                IdentityNumber = createCustomerDto.IdentityNumber,
                IdentityType = createCustomerDto.IdentityType,
                DateOfBirth = createCustomerDto.DateOfBirth,
                Gender = createCustomerDto.Gender,
                Notes = createCustomerDto.Notes,
                CreatedDate = DateTime.Now
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer created successfully with ID: {CustomerId}", customer.Id);

            return MapToCustomerDto(customer);
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return null;

            if (!string.IsNullOrEmpty(updateCustomerDto.Name))
                customer.Name = updateCustomerDto.Name;

            if (!string.IsNullOrEmpty(updateCustomerDto.Email))
                customer.Email = updateCustomerDto.Email;

            if (!string.IsNullOrEmpty(updateCustomerDto.Phone))
                customer.Phone = updateCustomerDto.Phone;

            if (updateCustomerDto.Address != null)
                customer.Address = updateCustomerDto.Address;

            if (!string.IsNullOrEmpty(updateCustomerDto.IdentityNumber))
                customer.IdentityNumber = updateCustomerDto.IdentityNumber;

            if (!string.IsNullOrEmpty(updateCustomerDto.IdentityType))
                customer.IdentityType = updateCustomerDto.IdentityType;

            if (updateCustomerDto.DateOfBirth.HasValue)
                customer.DateOfBirth = updateCustomerDto.DateOfBirth;

            if (!string.IsNullOrEmpty(updateCustomerDto.Gender))
                customer.Gender = updateCustomerDto.Gender;

            if (updateCustomerDto.Notes != null)
                customer.Notes = updateCustomerDto.Notes;

            customer.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer updated successfully with ID: {CustomerId}", customer.Id);

            return MapToCustomerDto(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return false;


            var hasSales = await _context.Sales.AnyAsync(s => s.CustomerId == id);
            if (hasSales)
            {
                _logger.LogWarning("Cannot delete customer with ID: {CustomerId} because they have sales records", id);
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer deleted successfully with ID: {CustomerId}", id);

            return true;
        }

        public async Task<CustomerWithSalesDto?> GetCustomerWithSalesAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Sales)
                    .ThenInclude(s => s.Car)
                .Include(c => c.Sales)
                    .ThenInclude(s => s.Employee)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return null;

            var customerDto = MapToCustomerDto(customer);
            var recentSales = customer.Sales
                .OrderByDescending(s => s.SaleDate)
                .Take(10)
                .Select(s => new CustomerSaleDto
                {
                    SaleId = s.Id,
                    CarInfo = $"{s.Car.Make} {s.Car.Model} {s.Car.Year}",
                    SalePrice = s.SalePrice,
                    SaleDate = s.SaleDate,
                    Status = s.Status,
                    EmployeeName = s.Employee.Name
                }).ToList();

            return new CustomerWithSalesDto
            {
                Id = customerDto.Id,
                Name = customerDto.Name,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address,
                IdentityNumber = customerDto.IdentityNumber,
                IdentityType = customerDto.IdentityType,
                DateOfBirth = customerDto.DateOfBirth,
                Gender = customerDto.Gender,
                Notes = customerDto.Notes,
                CreatedDate = customerDto.CreatedDate,
                UpdatedDate = customerDto.UpdatedDate,
                TotalPurchases = customerDto.TotalPurchases,
                TotalSpent = customerDto.TotalSpent,
                LastPurchaseDate = customerDto.LastPurchaseDate,
                RecentSales = recentSales
            };
        }

        public async Task<List<CustomerSummaryDto>> GetTopCustomersAsync(int count = 10)
        {
            var topCustomers = await _context.Customers
                .Include(c => c.Sales)
                .Where(c => c.Sales.Any())
                .Select(c => new CustomerSummaryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    TotalPurchases = c.Sales.Count(s => s.Status == "Completed"),
                    TotalSpent = c.Sales.Where(s => s.Status == "Completed").Sum(s => s.TotalAmount)
                })
                .OrderByDescending(c => c.TotalSpent)
                .Take(count)
                .ToListAsync();

            return topCustomers;
        }

        public async Task<bool> IsEmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.Customers.Where(c => c.Email == email);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> IsIdentityNumberExistsAsync(string identityNumber, int? excludeId = null)
        {
            var query = _context.Customers.Where(c => c.IdentityNumber == identityNumber);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return await query.AnyAsync();
        }

        private IQueryable<Customer> ApplySorting(IQueryable<Customer> query, string? sortBy, bool sortDescending)
        {
            return sortBy?.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                "email" => sortDescending ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email),
                "phone" => sortDescending ? query.OrderByDescending(c => c.Phone) : query.OrderBy(c => c.Phone),
                "dateofbirth" => sortDescending ? query.OrderByDescending(c => c.DateOfBirth) : query.OrderBy(c => c.DateOfBirth),
                "createddate" => sortDescending ? query.OrderByDescending(c => c.CreatedDate) : query.OrderBy(c => c.CreatedDate),
                _ => sortDescending ? query.OrderByDescending(c => c.CreatedDate) : query.OrderBy(c => c.CreatedDate)
            };
        }

        private CustomerDto MapToCustomerDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                IdentityNumber = customer.IdentityNumber,
                IdentityType = customer.IdentityType,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                Notes = customer.Notes,
                CreatedDate = customer.CreatedDate,
                UpdatedDate = customer.UpdatedDate,
                TotalPurchases = customer.Sales?.Count(s => s.Status == "Completed") ?? 0,
                TotalSpent = customer.Sales?.Where(s => s.Status == "Completed").Sum(s => s.TotalAmount) ?? 0,
                LastPurchaseDate = customer.Sales?.Where(s => s.Status == "Completed").Max(s => s.SaleDate)
            };
        }
    }
}

