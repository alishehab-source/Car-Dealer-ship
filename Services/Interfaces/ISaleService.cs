using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Sale;

namespace CarDealershipAPI.Services.Interfaces
{
    public interface ISaleService
    {
        Task<PagedResult<SaleDto>> GetAllSalesAsync(SaleSearchDto searchDto);
        Task<SaleDto?> GetSaleByIdAsync(int id);
        Task<SaleDetailsDto?> GetSaleDetailsAsync(int id);
        Task<SaleDto> CreateSaleAsync(CreateSaleDto createSaleDto);
        Task<SaleDto?> UpdateSaleAsync(int id, UpdateSaleDto updateSaleDto);
        Task<bool> DeleteSaleAsync(int id);
        Task<bool> CompleteSaleAsync(int id);
        Task<bool> CancelSaleAsync(int id);
        Task<SaleInvoiceDto?> GetSaleInvoiceAsync(int id);
        Task<List<SaleAnalyticsDto>> GetSalesAnalyticsAsync(DateTime fromDate, DateTime toDate);
        Task<MonthlySalesReportDto> GetMonthlySalesReportAsync(int month, int year);
    }

}
