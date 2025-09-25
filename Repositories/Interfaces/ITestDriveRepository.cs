using CarDealershipAPI.Models;

namespace CarDealershipAPI.Repositories.Interfaces
{
    public interface ITestDriveRepository : IRepository<TestDrive>
    {
        Task<IEnumerable<TestDrive>> GetTestDrivesByStatusAsync(string status);
        Task<IEnumerable<TestDrive>> GetTestDrivesByCustomerAsync(int customerId);
        Task<IEnumerable<TestDrive>> GetTestDrivesByCarAsync(int carId);
        Task<IEnumerable<TestDrive>> GetTestDrivesByEmployeeAsync(int employeeId);
        Task<IEnumerable<TestDrive>> GetTestDrivesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TestDrive>> GetScheduledTestDrivesAsync();
        Task<bool> IsCarAvailableForTestDriveAsync(int carId, DateTime scheduledDateTime, int durationMinutes);
        Task<TestDrive?> GetTestDriveWithDetailsAsync(int testDriveId);
    }
}
