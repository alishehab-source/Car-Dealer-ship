using CarDealershipAPI.Models;

namespace CarDealershipAPI.Repositories.Interfaces
{
    public interface ISaleRepository : IRepository<Sale>
    {
        Task<IEnumerable<Sale>> GetSalesByStatusAsync(string status);
        Task<IEnumerable<Sale>> GetSalesByCustomerAsync(int customerId);
        Task<IEnumerable<Sale>> GetSalesByEmployeeAsync(int employeeId);
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesAmountAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetEmployeeSalesAmountAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Sale>> GetPendingSalesAsync();
        Task<Sale?> GetSaleWithDetailsAsync(int saleId);
    }

}
