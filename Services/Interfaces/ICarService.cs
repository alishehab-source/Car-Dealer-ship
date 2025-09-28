using CarDealershipAPI.DTOs.Car;
using CarDealershipAPI.DTOs.common;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface ICarService
    {
        Task<PagedResult<CarDto>> GetAllCarsAsync(CarSearchDto searchDto);
        Task<CarDto?> GetCarByIdAsync(int id);
        Task<CarDto> CreateCarAsync(CreateCarDto createCarDto);
        Task<CarDto?> UpdateCarAsync(int id, UpdateCarDto updateCarDto);
        Task<bool> DeleteCarAsync(int id);
        Task<List<CarSummaryDto>> GetAvailableCarsAsync();
        Task<bool> IsCarAvailableAsync(int carId);
        Task<bool> MarkCarAsSoldAsync(int carId);
        Task<bool> MarkCarAsReservedAsync(int carId);
        Task<FilterOptionsDto> GetFilterOptionsAsync();
    }

}
