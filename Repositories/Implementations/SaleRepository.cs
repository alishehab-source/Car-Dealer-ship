using CarDealershipAPI.Data;
using CarDealershipAPI.Models;
using CarDealershipAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Repositories.Implementations
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(CarDealershipContext context, ILogger<Repository<Sale>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Sale>> GetSalesByStatusAsync(string status)
        {
            try
            {
                return await GetAllAsync(s => s.Status == status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب المبيعات بالحالة {Status}", status);
                throw new Exception($"خطأ في جلب المبيعات بالحالة {status}", ex);
            }
        }

        public async Task<IEnumerable<Sale>> GetSalesByCustomerAsync(int customerId)
        {
            try
            {
                return await GetAllAsync(s => s.CustomerId == customerId,
                    s => s.Car,
                    s => s.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب مبيعات العميل {CustomerId}", customerId);
                throw new Exception($"خطأ في جلب مبيعات العميل {customerId}", ex);
            }
        }

        public async Task<IEnumerable<Sale>> GetSalesByEmployeeAsync(int employeeId)
        {
            try
            {
                return await GetAllAsync(s => s.EmployeeId == employeeId,
                    s => s.Car,
                    s => s.Customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب مبيعات الموظف {EmployeeId}", employeeId);
                throw new Exception($"خطأ في جلب مبيعات الموظف {employeeId}", ex);
            }
        }

        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await GetAllAsync(s => s.SaleDate >= startDate && s.SaleDate <= endDate,
                    s => s.Car,
                    s => s.Customer,
                    s => s.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب المبيعات في الفترة من {StartDate} إلى {EndDate}", startDate, endDate);
                throw new Exception($"خطأ في جلب المبيعات في الفترة المحددة", ex);
            }
        }

        public async Task<decimal> GetTotalSalesAmountAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = await _dbSet
                    .Where(s => s.SaleDate >= startDate &&
                               s.SaleDate <= endDate &&
                               s.Status == "Completed")
                    .ToListAsync();

                return sales.Sum(s => s.TotalAmount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حساب إجمالي المبيعات في الفترة من {StartDate} إلى {EndDate}", startDate, endDate);
                throw new Exception("خطأ في حساب إجمالي المبيعات", ex);
            }
        }

        public async Task<decimal> GetEmployeeSalesAmountAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = await _dbSet
                    .Where(s => s.EmployeeId == employeeId &&
                               s.SaleDate >= startDate &&
                               s.SaleDate <= endDate &&
                               s.Status == "Completed")
                    .ToListAsync();

                return sales.Sum(s => s.TotalAmount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حساب مبيعات الموظف {EmployeeId} في الفترة من {StartDate} إلى {EndDate}", employeeId, startDate, endDate);
                throw new Exception($"خطأ في حساب مبيعات الموظف {employeeId}", ex);
            }
        }

        public async Task<IEnumerable<Sale>> GetPendingSalesAsync()
        {
            try
            {
                return await GetAllAsync(s => s.Status == "Pending",
                    s => s.Car,
                    s => s.Customer,
                    s => s.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب المبيعات المعلقة");
                throw new Exception("خطأ في جلب المبيعات المعلقة", ex);
            }
        }

        public async Task<Sale?> GetSaleWithDetailsAsync(int saleId)
        {
            try
            {
                return await GetByIdAsync(saleId,
                    s => s.Car,
                    s => s.Customer,
                    s => s.Employee,
                    s => s.TestDrive!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تفاصيل المبيعة {SaleId}", saleId);
                throw new Exception($"خطأ في جلب تفاصيل المبيعة {saleId}", ex);
            }
        }

        // Additional helper methods for better functionality

        public async Task<IEnumerable<Sale>> GetSalesByCarAsync(int carId)
        {
            try
            {
                return await GetAllAsync(s => s.CarId == carId,
                    s => s.Customer,
                    s => s.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب مبيعات السيارة {CarId}", carId);
                throw new Exception($"خطأ في جلب مبيعات السيارة {carId}", ex);
            }
        }

        public async Task<IEnumerable<Sale>> GetFinancedSalesAsync()
        {
            try
            {
                return await GetAllAsync(s => s.IsFinanced == true,
                    s => s.Car,
                    s => s.Customer,
                    s => s.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب المبيعات الممولة");
                throw new Exception("خطأ في جلب المبيعات الممولة", ex);
            }
        }

        public async Task<IEnumerable<Sale>> GetTradeInSalesAsync()
        {
            try
            {
                return await GetAllAsync(s => s.IsTradeIn == true,
                    s => s.Car,
                    s => s.Customer,
                    s => s.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب مبيعات التبادل التجاري");
                throw new Exception("خطأ في جلب مبيعات التبادل التجاري", ex);
            }
        }

        public async Task<decimal> GetAverageSalePriceAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = await _dbSet
                    .Where(s => s.SaleDate >= startDate &&
                               s.SaleDate <= endDate &&
                               s.Status == "Completed")
                    .ToListAsync();

                return sales.Any() ? sales.Average(s => s.SalePrice) : 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حساب متوسط سعر البيع في الفترة من {StartDate} إلى {EndDate}", startDate, endDate);
                throw new Exception("خطأ في حساب متوسط سعر البيع", ex);
            }
        }

        public async Task<int> GetSalesCountByStatusAsync(string status)
        {
            try
            {
                return await CountAsync(s => s.Status == status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في عد المبيعات بالحالة {Status}", status);
                throw new Exception($"خطأ في عد المبيعات بالحالة {status}", ex);
            }
        }

        public async Task<IEnumerable<Sale>> GetSalesByPaymentMethodAsync(string paymentMethod)
        {
            try
            {
                return await GetAllAsync(s => s.PaymentMethod == paymentMethod,
                    s => s.Car,
                    s => s.Customer,
                    s => s.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب المبيعات بطريقة الدفع {PaymentMethod}", paymentMethod);
                throw new Exception($"خطأ في جلب المبيعات بطريقة الدفع {paymentMethod}", ex);
            }
        }

        public async Task<decimal> GetTotalCommissionAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = await _dbSet
                    .Where(s => s.SaleDate >= startDate &&
                               s.SaleDate <= endDate &&
                               s.Status == "Completed" &&
                               s.EmployeeCommission.HasValue)
                    .ToListAsync();

                return sales.Sum(s => s.EmployeeCommission ?? 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حساب إجمالي العمولات في الفترة من {StartDate} إلى {EndDate}", startDate, endDate);
                throw new Exception("خطأ في حساب إجمالي العمولات", ex);
            }
        }

        public async Task<decimal> GetTotalProfitAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = await _dbSet
                    .Where(s => s.SaleDate >= startDate &&
                               s.SaleDate <= endDate &&
                               s.Status == "Completed" &&
                               s.ProfitMargin.HasValue)
                    .ToListAsync();

                return sales.Sum(s => s.ProfitMargin ?? 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حساب إجمالي الأرباح في الفترة من {StartDate} إلى {EndDate}", startDate, endDate);
                throw new Exception("خطأ في حساب إجمالي الأرباح", ex);
            }
        }

        public async Task MarkSaleAsCompletedAsync(int saleId)
        {
            try
            {
                var sale = await GetByIdAsync(saleId);
                if (sale != null)
                {
                    sale.Status = "Completed";
                    sale.CompletionDate = DateTime.Now;
                    sale.UpdatedDate = DateTime.Now;
                    Update(sale);
                    _logger.LogInformation("تم تحديث حالة المبيعة {SaleId} إلى مكتملة", saleId);
                }
                else
                {
                    _logger.LogWarning("لم يتم العثور على المبيعة {SaleId}", saleId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في تحديث حالة المبيعة {SaleId}", saleId);
                throw new Exception($"خطأ في تحديث حالة المبيعة {saleId}", ex);
            }
        }

        public async Task CancelSaleAsync(int saleId, string cancellationReason)
        {
            try
            {
                var sale = await GetByIdAsync(saleId);
                if (sale != null)
                {
                    sale.Status = "Cancelled";
                    sale.CancellationReason = cancellationReason;
                    sale.CancellationDate = DateTime.Now;
                    sale.UpdatedDate = DateTime.Now;
                    Update(sale);
                    _logger.LogInformation("تم إلغاء المبيعة {SaleId} بسبب: {Reason}", saleId, cancellationReason);
                }
                else
                {
                    _logger.LogWarning("لم يتم العثور على المبيعة {SaleId}", saleId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في إلغاء المبيعة {SaleId}", saleId);
                throw new Exception($"خطأ في إلغاء المبيعة {saleId}", ex);
            }
        }
    }
}
