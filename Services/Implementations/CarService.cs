using CarDealershipAPI.Data;
using CarDealershipAPI.DTOs.Car;
using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.Models;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly CarDealershipDbContext _context;
        private readonly ILogger<CarService> _logger;

        public CarService(CarDealershipDbContext context, ILogger<CarService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedResult<CarDto>> GetAllCarsAsync(CarSearchDto searchDto)
        {
            var query = _context.Cars.AsQueryable();


            if (!string.IsNullOrEmpty(searchDto.Make))
                query = query.Where(c => c.Make.Contains(searchDto.Make));

            if (!string.IsNullOrEmpty(searchDto.Model))
                query = query.Where(c => c.Model.Contains(searchDto.Model));

            if (searchDto.MinYear.HasValue)
                query = query.Where(c => c.Year >= searchDto.MinYear.Value);

            if (searchDto.MaxYear.HasValue)
                query = query.Where(c => c.Year <= searchDto.MaxYear.Value);

            if (searchDto.MinPrice.HasValue)
                query = query.Where(c => c.Price >= searchDto.MinPrice.Value);

            if (searchDto.MaxPrice.HasValue)
                query = query.Where(c => c.Price <= searchDto.MaxPrice.Value);

            if (!string.IsNullOrEmpty(searchDto.Color))
                query = query.Where(c => c.Color == searchDto.Color);

            if (searchDto.MaxMileage.HasValue)
                query = query.Where(c => c.Mileage <= searchDto.MaxMileage.Value);

            if (!string.IsNullOrEmpty(searchDto.FuelType))
                query = query.Where(c => c.FuelType == searchDto.FuelType);

            if (!string.IsNullOrEmpty(searchDto.Transmission))
                query = query.Where(c => c.Transmission == searchDto.Transmission);

            if (!string.IsNullOrEmpty(searchDto.Condition))
                query = query.Where(c => c.Condition == searchDto.Condition);

            if (!string.IsNullOrEmpty(searchDto.Status))
                query = query.Where(c => c.Status == searchDto.Status);

            query = ApplySorting(query, searchDto.SortBy, searchDto.SortDescending);

            var totalRecords = await query.CountAsync();

            var cars = await query
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Include(c => c.Sales)
                .ToListAsync();


            var carDtos = cars.Select(MapToCarDto).ToList();

            return new PagedResult<CarDto>(carDtos, totalRecords, searchDto.Page, searchDto.PageSize);
        }

        public async Task<CarDto?> GetCarByIdAsync(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Sales)
                .FirstOrDefaultAsync(c => c.Id == id);

            return car != null ? MapToCarDto(car) : null;
        }

        public async Task<CarDto> CreateCarAsync(CreateCarDto createCarDto)
        {
            var car = new Car
            {
                Make = createCarDto.Make,
                Model = createCarDto.Model,
                Year = createCarDto.Year,
                Price = createCarDto.Price,
                VIN = createCarDto.VIN,
                Color = createCarDto.Color,
                Mileage = createCarDto.Mileage,
                FuelType = createCarDto.FuelType,
                Transmission = createCarDto.Transmission,
                Condition = createCarDto.Condition,
                Status = createCarDto.Status,
                Doors = createCarDto.Doors,
                Seats = createCarDto.Seats,
                Description = createCarDto.Description,
                CreatedDate = DateTime.Now
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Car created successfully with ID: {CarId}", car.Id);

            return MapToCarDto(car);
        }

        public async Task<CarDto?> UpdateCarAsync(int id, UpdateCarDto updateCarDto)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return null;

            if (!string.IsNullOrEmpty(updateCarDto.Make))
                car.Make = updateCarDto.Make;

            if (!string.IsNullOrEmpty(updateCarDto.Model))
                car.Model = updateCarDto.Model;

            if (updateCarDto.Year.HasValue)
                car.Year = updateCarDto.Year.Value;

            if (updateCarDto.Price.HasValue)
                car.Price = updateCarDto.Price.Value;

            if (updateCarDto.VIN != null)
                car.VIN = updateCarDto.VIN;

            if (updateCarDto.Color != null)
                car.Color = updateCarDto.Color;

            if (updateCarDto.Mileage.HasValue)
                car.Mileage = updateCarDto.Mileage.Value;

            if (!string.IsNullOrEmpty(updateCarDto.FuelType))
                car.FuelType = updateCarDto.FuelType;

            if (!string.IsNullOrEmpty(updateCarDto.Transmission))
                car.Transmission = updateCarDto.Transmission;

            if (!string.IsNullOrEmpty(updateCarDto.Condition))
                car.Condition = updateCarDto.Condition;

            if (!string.IsNullOrEmpty(updateCarDto.Status))
                car.Status = updateCarDto.Status;

            if (updateCarDto.Doors.HasValue)
                car.Doors = updateCarDto.Doors.Value;

            if (updateCarDto.Seats.HasValue)
                car.Seats = updateCarDto.Seats.Value;

            if (updateCarDto.Description != null)
                car.Description = updateCarDto.Description;

            car.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Car updated successfully with ID: {CarId}", car.Id);

            return MapToCarDto(car);
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return false;

  
            var hasSales = await _context.Sales.AnyAsync(s => s.CarId == id);
            if (hasSales)
            {
                _logger.LogWarning("Cannot delete car with ID: {CarId} because it has sales records", id);
                return false;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Car deleted successfully with ID: {CarId}", id);

            return true;
        }

        public async Task<List<CarSummaryDto>> GetAvailableCarsAsync()
        {
            var cars = await _context.Cars
                .Where(c => c.Status == "Available")
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ThenBy(c => c.Year)
                .ToListAsync();

            return cars.Select(c => new CarSummaryDto
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                Price = c.Price,
                Status = c.Status
            }).ToList();
        }
        public async Task<bool> IsCarAvailableAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            return car != null && car.Status == "Available";
        }

        public async Task<bool> MarkCarAsSoldAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null || car.Status != "Available")
                return false;

            car.Status = "Sold";
            car.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Car marked as sold with ID: {CarId}", carId);

            return true;
        }

        public async Task<bool> MarkCarAsReservedAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null || car.Status != "Available")
                return false;

            car.Status = "Reserved";
            car.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Car marked as reserved with ID: {CarId}", carId);

            return true;
        }

        public async Task<FilterOptionsDto> GetFilterOptionsAsync()
        {
            var cars = await _context.Cars.ToListAsync();

            return new FilterOptionsDto
            {
                Makes = cars.Select(c => c.Make).Distinct().OrderBy(m => m).ToList(),
                Models = cars.Select(c => c.Model).Distinct().OrderBy(m => m).ToList(),
                Colors = cars.Where(c => !string.IsNullOrEmpty(c.Color))
                            .Select(c => c.Color!).Distinct().OrderBy(c => c).ToList(),
                FuelTypes = cars.Select(c => c.FuelType).Distinct().OrderBy(f => f).ToList(),
                Transmissions = cars.Select(c => c.Transmission).Distinct().OrderBy(t => t).ToList(),
                Conditions = cars.Select(c => c.Condition).Distinct().OrderBy(c => c).ToList(),
                Statuses = cars.Select(c => c.Status).Distinct().OrderBy(s => s).ToList(),
                PriceRange = new PriceRange
                {
                    Min = cars.Any() ? cars.Min(c => c.Price) : 0,
                    Max = cars.Any() ? cars.Max(c => c.Price) : 0
                },
                YearRange = new YearRange
                {
                    Min = cars.Any() ? cars.Min(c => c.Year) : 1980,
                    Max = cars.Any() ? cars.Max(c => c.Year) : DateTime.Now.Year
                }
            };
        }

        private IQueryable<Car> ApplySorting(IQueryable<Car> query, string? sortBy, bool sortDescending)
        {
            return sortBy?.ToLower() switch
            {
                "make" => sortDescending ? query.OrderByDescending(c => c.Make) : query.OrderBy(c => c.Make),
                "model" => sortDescending ? query.OrderByDescending(c => c.Model) : query.OrderBy(c => c.Model),
                "year" => sortDescending ? query.OrderByDescending(c => c.Year) : query.OrderBy(c => c.Year),
                "price" => sortDescending ? query.OrderByDescending(c => c.Price) : query.OrderBy(c => c.Price),
                "mileage" => sortDescending ? query.OrderByDescending(c => c.Mileage) : query.OrderBy(c => c.Mileage),
                "createddate" => sortDescending ? query.OrderByDescending(c => c.CreatedDate) : query.OrderBy(c => c.CreatedDate),
                _ => sortDescending ? query.OrderByDescending(c => c.CreatedDate) : query.OrderBy(c => c.CreatedDate)
            };
        }

        private CarDto MapToCarDto(Car car)
        {
            return new CarDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                VIN = car.VIN,
                Color = car.Color,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                Transmission = car.Transmission,
                Condition = car.Condition,
                Status = car.Status,
                Doors = car.Doors,
                Seats = car.Seats,
                Description = car.Description,
                CreatedDate = car.CreatedDate,
                UpdatedDate = car.UpdatedDate,
                SalesCount = car.Sales?.Count ?? 0
            };
        }
    }
}

