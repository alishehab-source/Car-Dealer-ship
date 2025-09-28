using CarDealershipAPI.Data;
using CarDealershipAPI.DTOs.Car;
using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Sale;
using CarDealershipAPI.Models;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Services.Implementations
{

    public class SaleService : ISaleService
    {
        private readonly CarDealershipDbContext _context;
        private readonly ILogger<SaleService> _logger;
        private readonly ICarService _carService;

        public SaleService(CarDealershipDbContext context, ILogger<SaleService> logger, ICarService carService)
        {
            _context = context;
            _logger = logger;
            _carService = carService;
        }

        public async Task<PagedResult<SaleDto>> GetAllSalesAsync(SaleSearchDto searchDto)
        {
            var query = _context.Sales
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .AsQueryable();

            if (searchDto.CarId.HasValue)
                query = query.Where(s => s.CarId == searchDto.CarId.Value);

            if (searchDto.CustomerId.HasValue)
                query = query.Where(s => s.CustomerId == searchDto.CustomerId.Value);

            if (searchDto.EmployeeId.HasValue)
                query = query.Where(s => s.EmployeeId == searchDto.EmployeeId.Value);

            if (searchDto.SaleDateFrom.HasValue)
                query = query.Where(s => s.SaleDate >= searchDto.SaleDateFrom.Value);

            if (searchDto.SaleDateTo.HasValue)
                query = query.Where(s => s.SaleDate <= searchDto.SaleDateTo.Value);

            if (searchDto.MinSalePrice.HasValue)
                query = query.Where(s => s.SalePrice >= searchDto.MinSalePrice.Value);

            if (searchDto.MaxSalePrice.HasValue)
                query = query.Where(s => s.SalePrice <= searchDto.MaxSalePrice.Value);

            if (!string.IsNullOrEmpty(searchDto.PaymentMethod))
                query = query.Where(s => s.PaymentMethod == searchDto.PaymentMethod);

            if (!string.IsNullOrEmpty(searchDto.Status))
                query = query.Where(s => s.Status == searchDto.Status);

            if (!string.IsNullOrEmpty(searchDto.InvoiceNumber))
                query = query.Where(s => s.InvoiceNumber != null && s.InvoiceNumber.Contains(searchDto.InvoiceNumber));

            if (!string.IsNullOrEmpty(searchDto.CustomerName))
                query = query.Where(s => s.Customer.Name.Contains(searchDto.CustomerName));

            if (!string.IsNullOrEmpty(searchDto.EmployeeName))
                query = query.Where(s => s.Employee.Name.Contains(searchDto.EmployeeName));

            if (!string.IsNullOrEmpty(searchDto.CarMake))
                query = query.Where(s => s.Car.Make.Contains(searchDto.CarMake));

            if (!string.IsNullOrEmpty(searchDto.CarModel))
                query = query.Where(s => s.Car.Model.Contains(searchDto.CarModel));

            query = ApplySorting(query, searchDto.SortBy, searchDto.SortDescending);

            var totalRecords = await query.CountAsync();

 
            var sales = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .ToListAsync();


            var saleDtos = sales.Select(MapToSaleDto).ToList();

            return new PagedResult<SaleDto>(saleDtos, totalRecords, searchDto.Page, searchDto.PageSize);
        }

        public async Task<SaleDto?> GetSaleByIdAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sale != null ? MapToSaleDto(sale) : null;
        }

        public async Task<SaleDetailsDto?> GetSaleDetailsAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return null;

            return new SaleDetailsDto
            {
                Id = sale.Id,
                CarId = sale.CarId,
                CustomerId = sale.CustomerId,
                EmployeeId = sale.EmployeeId,
                SaleDate = sale.SaleDate,
                SalePrice = sale.SalePrice,
                Discount = sale.Discount,
                Tax = sale.Tax,
                TaxRate = sale.TaxRate,
                TotalAmount = sale.TotalAmount,
                PaymentMethod = sale.PaymentMethod,
                Status = sale.Status,
                InvoiceNumber = sale.InvoiceNumber,
                CompletionDate = sale.CompletionDate,
                Notes = sale.Notes,
                EmployeeCommission = sale.EmployeeCommission,
                CreatedDate = sale.CreatedDate,
                UpdatedDate = sale.UpdatedDate,
                CarInfo = $"{sale.Car.Make} {sale.Car.Model} {sale.Car.Year}",
                CustomerName = sale.Customer.Name,
                EmployeeName = sale.Employee.Name,
                CarMake = sale.Car.Make,
                CarModel = sale.Car.Model,
                CarYear = sale.Car.Year,
                CarColor = sale.Car.Color,
                CarVIN = sale.Car.VIN,
                CustomerEmail = sale.Customer.Email,
                CustomerPhone = sale.Customer.Phone,
                EmployeeEmail = sale.Employee.Email,
                EmployeeDepartment = sale.Employee.Department
            };
        }

        public async Task<SaleDto> CreateSaleAsync(CreateSaleDto createSaleDto)
        {
            var isCarAvailable = await _carService.IsCarAvailableAsync(createSaleDto.CarId);
            if (!isCarAvailable)
            {
                throw new InvalidOperationException("Car is not available for sale");
            }

            var sale = new Sale
            {
                CarId = createSaleDto.CarId,
                CustomerId = createSaleDto.CustomerId,
                EmployeeId = createSaleDto.EmployeeId,
                SaleDate = createSaleDto.SaleDate,
                SalePrice = createSaleDto.SalePrice,
                Discount = createSaleDto.Discount,
                TaxRate = createSaleDto.TaxRate,
                PaymentMethod = createSaleDto.PaymentMethod,
                Status = createSaleDto.Status,
                InvoiceNumber = createSaleDto.InvoiceNumber,
                CompletionDate = createSaleDto.CompletionDate,
                Notes = createSaleDto.Notes,
                CreatedDate = DateTime.Now
            };

            sale.CalculateTax();
            sale.CalculateTotalAmount();

            var employee = await _context.Employees.FindAsync(createSaleDto.EmployeeId);
            if (employee != null)
            {
                sale.Employee = employee;
                sale.CalculateEmployeeCommission();
            }

            _context.Sales.Add(sale);

            if (sale.Status == "Completed")
            {
                await _carService.MarkCarAsSoldAsync(sale.CarId);
                sale.CompletionDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Sale created successfully with ID: {SaleId}", sale.Id);


            if (employee != null && sale.Status == "Completed")
            {
                await UpdateEmployeePerformance(employee.Id);
            }

            return await GetSaleByIdAsync(sale.Id) ?? throw new InvalidOperationException("Failed to retrieve created sale");
        }

        public async Task<SaleDto?> UpdateSaleAsync(int id, UpdateSaleDto updateSaleDto)
        {
            var sale = await _context.Sales
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return null;

            var oldStatus = sale.Status;


            if (updateSaleDto.SaleDate.HasValue)
                sale.SaleDate = updateSaleDto.SaleDate.Value;

            if (updateSaleDto.SalePrice.HasValue)
                sale.SalePrice = updateSaleDto.SalePrice.Value;

            if (updateSaleDto.Discount.HasValue)
                sale.Discount = updateSaleDto.Discount.Value;

            if (updateSaleDto.TaxRate.HasValue)
                sale.TaxRate = updateSaleDto.TaxRate.Value;

            if (!string.IsNullOrEmpty(updateSaleDto.PaymentMethod))
                sale.PaymentMethod = updateSaleDto.PaymentMethod;

            if (!string.IsNullOrEmpty(updateSaleDto.Status))
                sale.Status = updateSaleDto.Status;

            if (updateSaleDto.InvoiceNumber != null)
                sale.InvoiceNumber = updateSaleDto.InvoiceNumber;

            if (updateSaleDto.CompletionDate.HasValue)
                sale.CompletionDate = updateSaleDto.CompletionDate.Value;

            if (updateSaleDto.Notes != null)
                sale.Notes = updateSaleDto.Notes;


            sale.CalculateTax();
            sale.CalculateTotalAmount();
            sale.CalculateEmployeeCommission();


            if (oldStatus != sale.Status)
            {
                if (sale.Status == "Completed" && oldStatus != "Completed")
                {
                    await _carService.MarkCarAsSoldAsync(sale.CarId);
                    sale.CompletionDate = DateTime.Now;
                }
                else if (sale.Status == "Cancelled")
                {
                    if (oldStatus == "Completed")
                    {
                        var car = await _context.Cars.FindAsync(sale.CarId);
                        if (car != null)
                        {
                            car.Status = "Available";
                            car.UpdatedDate = DateTime.Now;
                        }
                    }
                }
            }

            sale.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Sale updated successfully with ID: {SaleId}", sale.Id);

            
            if (sale.Status == "Completed" && oldStatus != "Completed")
            {
                await UpdateEmployeePerformance(sale.EmployeeId);
            }

            return await GetSaleByIdAsync(sale.Id);
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
                return false;


            if (sale.Status == "Completed")
            {
                var car = await _context.Cars.FindAsync(sale.CarId);
                if (car != null)
                {
                    car.Status = "Available";
                    car.UpdatedDate = DateTime.Now;
                }
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Sale deleted successfully with ID: {SaleId}", id);

            return true;
        }

        public async Task<bool> CompleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null || sale.Status == "Completed")
                return false;

            sale.Status = "Completed";
            sale.CompletionDate = DateTime.Now;
            sale.UpdatedDate = DateTime.Now;

            await _carService.MarkCarAsSoldAsync(sale.CarId);
            await _context.SaveChangesAsync();

            await UpdateEmployeePerformance(sale.EmployeeId);

            _logger.LogInformation("Sale completed successfully with ID: {SaleId}", id);

            return true;
        }

        public async Task<bool> CancelSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null || sale.Status == "Cancelled")
                return false;

            var wasCompleted = sale.Status == "Completed";
            sale.Status = "Cancelled";
            sale.UpdatedDate = DateTime.Now;

            if (wasCompleted)
            {
                var car = await _context.Cars.FindAsync(sale.CarId);
                if (car != null)
                {
                    car.Status = "Available";
                    car.UpdatedDate = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Sale cancelled successfully with ID: {SaleId}", id);

            return true;
        }

        public async Task<SaleInvoiceDto?> GetSaleInvoiceAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return null;

            return new SaleInvoiceDto
            {
                SaleId = sale.Id,
                InvoiceNumber = sale.InvoiceNumber ?? $"INV-{sale.Id:D6}",
                SaleDate = sale.SaleDate,
                CustomerName = sale.Customer.Name,
                CustomerEmail = sale.Customer.Email,
                CustomerPhone = sale.Customer.Phone,
                CustomerAddress = sale.Customer.Address,
                CarMake = sale.Car.Make,
                CarModel = sale.Car.Model,
                CarYear = sale.Car.Year,
                CarVIN = sale.Car.VIN,
                CarColor = sale.Car.Color,
                EmployeeName = sale.Employee.Name,
                SalePrice = sale.SalePrice,
                Discount = sale.Discount,
                Tax = sale.Tax,
                TaxRate = sale.TaxRate,
                TotalAmount = sale.TotalAmount,
                PaymentMethod = sale.PaymentMethod,
                Notes = sale.Notes
            };
        }

        public async Task<List<SaleAnalyticsDto>> GetSalesAnalyticsAsync(DateTime fromDate, DateTime toDate)
        {
            var analytics = new List<SaleAnalyticsDto>();
            var currentDate = fromDate.Date;

            while (currentDate <= toDate.Date)
            {
                var dayEnd = currentDate.AddDays(1);
                var daySales = await _context.Sales
                    .Include(s => s.Employee)
                    .Where(s => s.SaleDate >= currentDate && s.SaleDate < dayEnd)
                    .ToListAsync();

                var analytics_item = new SaleAnalyticsDto
                {
                    Period = currentDate,
                    TotalSales = daySales.Count,
                    TotalRevenue = daySales.Sum(s => s.TotalAmount),
                    TotalCommissions = daySales.Sum(s => s.EmployeeCommission ?? 0),
                    AverageSaleValue = daySales.Any() ? daySales.Average(s => s.TotalAmount) : 0,
                    CompletedSales = daySales.Count(s => s.Status == "Completed"),
                    PendingSales = daySales.Count(s => s.Status == "Pending"),
                    CancelledSales = daySales.Count(s => s.Status == "Cancelled"),
                    SalesByPaymentMethod = daySales.GroupBy(s => s.PaymentMethod)
                        .ToDictionary(g => g.Key, g => g.Count()),
                    RevenueByEmployees = daySales.GroupBy(s => s.Employee.Name)
                        .ToDictionary(g => g.Key, g => g.Sum(s => s.TotalAmount))
                };

                analytics.Add(analytics_item);
                currentDate = currentDate.AddDays(1);
            }

            return analytics;
        }

        public async Task<MonthlySalesReportDto> GetMonthlySalesReportAsync(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var monthlySales = await _context.Sales
                .Include(s => s.Car)
                .Include(s => s.Employee)
                .Where(s => s.SaleDate >= startDate && s.SaleDate < endDate)
                .ToListAsync();

            var topEmployees = monthlySales
                .GroupBy(s => new { s.EmployeeId, s.Employee.Name })
                .Select(g => new TopPerformerDto
                {
                    EmployeeId = g.Key.EmployeeId,
                    EmployeeName = g.Key.Name,
                    SalesCount = g.Count(),
                    SalesValue = g.Sum(s => s.TotalAmount),
                    Commission = g.Sum(s => s.EmployeeCommission ?? 0)
                })
                .OrderByDescending(e => e.SalesValue)
                .Take(5)
                .ToList();

            var topCars = monthlySales
                .GroupBy(s => new { s.Car.Make, s.Car.Model, s.Car.Year })
                .Select(g => new TopSellingCarDto
                {
                    Make = g.Key.Make,
                    Model = g.Key.Model,
                    Year = g.Key.Year,
                    SalesCount = g.Count(),
                    TotalRevenue = g.Sum(s => s.TotalAmount)
                })
                .OrderByDescending(c => c.SalesCount)
                .Take(5)
                .ToList();

            return new MonthlySalesReportDto
            {
                Month = month,
                Year = year,
                TotalSales = monthlySales.Count,
                TotalRevenue = monthlySales.Sum(s => s.TotalAmount),
                TotalCommissions = monthlySales.Sum(s => s.EmployeeCommission ?? 0),
                AverageSaleValue = monthlySales.Any() ? monthlySales.Average(s => s.TotalAmount) : 0,
                TopEmployees = topEmployees,
                TopSellingCars = topCars
            };
        }

        private async Task UpdateEmployeePerformance(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Sales)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee != null)
            {
                var completedSales = employee.Sales.Where(s => s.Status == "Completed");
                employee.TotalSales = completedSales.Count();
                employee.TotalSalesValue = completedSales.Sum(s => s.SalePrice);
                employee.TotalCommissionEarned = completedSales.Sum(s => s.EmployeeCommission ?? 0);
                employee.LastSaleDate = completedSales.Any() ? completedSales.Max(s => s.SaleDate) : null;
                employee.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }


        private IQueryable<Sale> ApplySorting(IQueryable<Sale> query, string? sortBy, bool sortDescending)
        {
            return sortBy?.ToLower() switch
            {
                "saledate" => sortDescending ? query.OrderByDescending(s => s.SaleDate) : query.OrderBy(s => s.SaleDate),
                "saleprice" => sortDescending ? query.OrderByDescending(s => s.SalePrice) : query.OrderBy(s => s.SalePrice),
                "totalamount" => sortDescending ? query.OrderByDescending(s => s.TotalAmount) : query.OrderBy(s => s.TotalAmount),
                "customer" => sortDescending ? query.OrderByDescending(s => s.Customer.Name) : query.OrderBy(s => s.Customer.Name),
                "employee" => sortDescending ? query.OrderByDescending(s => s.Employee.Name) : query.OrderBy(s => s.Employee.Name),
                "car" => sortDescending ? query.OrderByDescending(s => s.Car.Make).ThenByDescending(s => s.Car.Model) : query.OrderBy(s => s.Car.Make).ThenBy(s => s.Car.Model),
                "status" => sortDescending ? query.OrderByDescending(s => s.Status) : query.OrderBy(s => s.Status),
                "createddate" => sortDescending ? query.OrderByDescending(s => s.CreatedDate) : query.OrderBy(s => s.CreatedDate),
                _ => sortDescending ? query.OrderByDescending(s => s.SaleDate) : query.OrderBy(s => s.SaleDate)
            };
        }

        private SaleDto MapToSaleDto(Sale sale)
        {
            return new SaleDto
            {
                Id = sale.Id,
                CarId = sale.CarId,
                CustomerId = sale.CustomerId,
                EmployeeId = sale.EmployeeId,
                SaleDate = sale.SaleDate,
                SalePrice = sale.SalePrice,
                Discount = sale.Discount,
                Tax = sale.Tax,
                TaxRate = sale.TaxRate,
                TotalAmount = sale.TotalAmount,
                PaymentMethod = sale.PaymentMethod,
                Status = sale.Status,
                InvoiceNumber = sale.InvoiceNumber,
                CompletionDate = sale.CompletionDate,
                Notes = sale.Notes,
                EmployeeCommission = sale.EmployeeCommission,
                CreatedDate = sale.CreatedDate,
                UpdatedDate = sale.UpdatedDate,
                CarInfo = $"{sale.Car.Make} {sale.Car.Model} {sale.Car.Year}",
                CustomerName = sale.Customer.Name,
                EmployeeName = sale.Employee.Name
            };
        }
    }
}
   
