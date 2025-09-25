using CarDealershipAPI.Data;
using CarDealershipAPI.Models;
using CarDealershipAPI.Repositories.Interfaces;

namespace CarDealershipAPI.Repositories.Implementations
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(CarDealershipContext context, ILogger<Repository<Car>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Car>> GetAvailableCarsAsync()
        {
            return await GetAllAsync(c => c.Status == "Available");
        }

        public async Task<IEnumerable<Car>> GetCarsByMakeAsync(string make)
        {
            return await GetAllAsync(c => c.Make.ToLower() == make.ToLower());
        }

        public async Task<IEnumerable<Car>> GetCarsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await GetAllAsync(c => c.Price >= minPrice && c.Price <= maxPrice);
        }

        public async Task<IEnumerable<Car>> GetCarsByYearRangeAsync(int minYear, int maxYear)
        {
            return await GetAllAsync(c => c.Year >= minYear && c.Year <= maxYear);
        }

        public async Task<Car?> GetCarByVINAsync(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin)) return null;
            return await GetFirstOrDefaultAsync(c => c.VIN == vin.ToUpper());
        }

        public async Task<IEnumerable<Car>> SearchCarsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return await GetAllAsync();

            return await GetAllAsync(c =>
                c.Make.Contains(searchTerm) ||
                c.Model.Contains(searchTerm) ||
                c.Color!.Contains(searchTerm) ||
                c.Description!.Contains(searchTerm));
        }

        public async Task<bool> IsVINExistsAsync(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin)) return false;
            return await ExistsAsync(c => c.VIN == vin.ToUpper());
        }

        public async Task MarkAsSoldAsync(int carId)
        {
            var car = await GetByIdAsync(carId);
            if (car != null)
            {
                car.Status = "Sold";
                car.SoldDate = DateTime.Now;
                car.UpdatedDate = DateTime.Now;
                Update(car);
            }
        }

        public async Task MarkAsAvailableAsync(int carId)
        {
            var car = await GetByIdAsync(carId);
            if (car != null)
            {
                car.Status = "Available";
                car.SoldDate = null;
                car.UpdatedDate = DateTime.Now;
                Update(car);
            }
        }

    }
}
