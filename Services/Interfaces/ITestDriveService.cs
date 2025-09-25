using CarDealershipAPI.DTOs.Customer;
using CarDealershipAPI.DTOs.TestDrive;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface ITestDriveService
    {
        Task<TestDriveResponseDto> ScheduleTestDriveAsync(CreateTestDriveDto dto);
        Task<TestDriveResponseDto?> GetTestDriveByIdAsync(int id);
        Task<TestDriveResponseDto> UpdateTestDriveAsync(UpdateTestDriveDto dto);
        Task<bool> CancelTestDriveAsync(TestDriveCancelDto dto);
        Task<bool> RescheduleTestDriveAsync(TestDriveRescheduleDto dto);

        Task<IEnumerable<TestDriveListDto>> SearchTestDrivesAsync(TestDriveSearchDto searchDto);
        Task<TestDriveDetailsDto?> GetTestDriveDetailsAsync(int id);
        Task<bool> StartTestDriveAsync(int testDriveId, int mileageBefore, int fuelLevelBefore);
        Task<bool> CompleteTestDriveAsync(TestDriveCompleteDto dto);
        Task<bool> ReportIncidentAsync(int testDriveId, string incidentType, string details, decimal? repairCost);

        Task<bool> IsCarAvailableForTestDriveAsync(int carId, DateTime scheduledDateTime, int durationMinutes);
        Task<IEnumerable<DateTime>> GetAvailableTimeSlotsAsync(int carId, DateTime date, int durationMinutes);
        Task<bool> ValidateTestDriveScheduleAsync(CreateTestDriveDto dto);
        Task<IEnumerable<TestDriveListDto>> GetTodaysTestDrivesAsync();
        Task<IEnumerable<TestDriveListDto>> GetOverdueTestDrivesAsync();

        Task<bool> ValidateCustomerLicenseAsync(int customerId, string licenseNumber, DateTime expiryDate);
        Task<bool> CheckCustomerInsuranceAsync(int customerId, string policyNumber);
        Task<IEnumerable<TestDriveResponseDto>> GetCustomerTestDriveHistoryAsync(int customerId);
        Task<bool> SendTestDriveReminderAsync(int testDriveId);

        Task<bool> MarkTestDriveAsConvertedAsync(int testDriveId, int saleId);
        Task<IEnumerable<TestDriveListDto>> GetTestDrivesNeedingFollowUpAsync();
        Task<bool> ScheduleFollowUpAsync(int testDriveId, DateTime followUpDate, string notes);
        Task<decimal> CalculateTestDriveConversionRateAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<TestDriveResponseDto>> GetEmployeeTestDrivesAsync(int employeeId);
        Task<bool> AssignTestDriveToEmployeeAsync(int testDriveId, int employeeId);

        Task<IEnumerable<TestDriveResponseDto>> GetCarTestDriveHistoryAsync(int carId);
        Task<bool> UpdateCarConditionAfterTestDriveAsync(int carId, int mileageAfter, string condition);
        Task<decimal> CalculateTestDriveCostAsync(int testDriveId);

        Task<bool> GenerateTestDriveReportAsync(int testDriveId);
        Task<bool> ValidateDriverLicenseAsync(string licenseNumber, DateTime expiryDate);
        Task<bool> CheckInsuranceCoverageAsync(string policyNumber, DateTime expiryDate);
        Task<IEnumerable<TestDriveListDto>> GetTestDrivesWithIncidentsAsync();

        Task<TestDriveAnalyticsDto> GetTestDriveAnalyticsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MonthlyTestDriveDto>> GetMonthlyTestDriveReportAsync(int year);
        Task<IEnumerable<TestDriveByCarDto>> GetCarTestDrivePopularityAsync();
        Task<decimal> GetAverageTestDriveRatingAsync();
        Task<int> GetTotalTestDrivesCountAsync();

        Task<IEnumerable<TestDriveListDto>> GetRecommendedTestDrivesForCustomerAsync(int customerId);
        Task<IEnumerable<CustomerListDto>> GetPotentialTestDriveCustomersAsync(int carId);
    }

}
