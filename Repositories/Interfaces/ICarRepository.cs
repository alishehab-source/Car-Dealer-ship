using CarDealershipAPI.Models;

namespace CarDealershipAPI.Repositories.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetAvailableCarsAsync();
        Task<IEnumerable<Car>> GetCarsByMakeAsync(string make);
        Task<IEnumerable<Car>> GetCarsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Car>> GetCarsByYearRangeAsync(int minYear, int maxYear);
        Task<Car?> GetCarByVINAsync(string vin);
        Task<IEnumerable<Car>> SearchCarsAsync(string searchTerm);
        Task<bool> IsVINExistsAsync(string vin);
        Task MarkAsSoldAsync(int carId);
        Task MarkAsAvailableAsync(int carId);
    }

}
