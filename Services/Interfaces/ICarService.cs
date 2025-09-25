using CarDealershipAPI.DTO.Car;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarResponseDto> CreateCarAsync(CreateCarDto dto);
        Task<CarResponseDto?> GetCarByIdAsync(int id);
        Task<CarResponseDto> UpdateCarAsync(UpdateCarDto dto);
        Task<bool> DeleteCarAsync(int id);

        Task<IEnumerable<CarListDto>> GetAvailableCarsAsync();
        Task<IEnumerable<CarListDto>> SearchCarsAsync(CarSearchDto searchDto);
        Task<CarDetailsDto?> GetCarDetailsAsync(int id);
        Task<bool> ReserveCarAsync(int carId, int customerId, DateTime reservationDate);
        Task<bool> ReleaseReservationAsync(int carId);
        Task<bool> MarkCarAsSoldAsync(int carId, int saleId);
        Task<bool> MarkCarAsAvailableAsync(int carId);

        Task<bool> IsCarAvailableAsync(int carId);
        Task<bool> IsVINUniqueAsync(string vin, int? excludeCarId = null);
        Task<bool> ValidateCarDataAsync(CreateCarDto dto);
        Task<decimal> CalculateCarDepreciationAsync(int carId);
        Task<decimal> EstimateCarValueAsync(int carId);

        Task<CarStatisticsDto> GetCarStatisticsAsync(int carId);
        Task<IEnumerable<CarSummaryDto>> GetTopPerformingCarsAsync(int count = 10);
        Task<IEnumerable<CarSummaryDto>> GetSlowMovingCarsAsync(int daysThreshold = 90);
        Task<decimal> GetAverageCarPriceAsync();
        Task<int> GetTotalAvailableCarsCountAsync();

        Task<IEnumerable<CarListDto>> GetRecommendedCarsForCustomerAsync(int customerId);
        Task<IEnumerable<CarListDto>> GetSimilarCarsAsync(int carId, int count = 5);
    }


}
