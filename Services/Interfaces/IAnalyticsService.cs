using CarDealershipAPI.DTO.Car;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<object> GetDashboardSummaryAsync();
        Task<object> GetSalesKPIsAsync(DateTime startDate, DateTime endDate);
        Task<object> GetCustomerKPIsAsync();
        Task<object> GetInventoryKPIsAsync();
        Task<object> GetEmployeeKPIsAsync();

        Task<object> GenerateSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<object> GenerateCustomerAnalysisReportAsync();
        Task<object> GenerateInventoryReportAsync();
        Task<object> GeneratePerformanceReportAsync(int employeeId);

        Task<decimal> PredictMonthlySalesAsync();
        Task<IEnumerable<CarListDto>> PredictSlowMovingInventoryAsync();
        Task<decimal> PredictCustomerLifetimeValueAsync(int customerId);
        Task<string> PredictBestTimeToContactCustomerAsync(int customerId);
    }
}
