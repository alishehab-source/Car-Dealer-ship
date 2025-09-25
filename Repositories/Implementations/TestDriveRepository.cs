using CarDealershipAPI.Data;
using CarDealershipAPI.Models;
using CarDealershipAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Repositories.Implementations
{
    public class TestDriveRepository : Repository<TestDrive>, ITestDriveRepository
    {
        public TestDriveRepository(CarDealershipContext context, ILogger<Repository<TestDrive>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<TestDrive>> GetTestDrivesByStatusAsync(string status)
        {
            try
            {
                return await GetAllAsync(td => td.Status == status,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة بالحالة {Status}", status);
                throw new Exception($"خطأ في جلب تجارب القيادة بالحالة {status}", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetTestDrivesByCustomerAsync(int customerId)
        {
            try
            {
                return await GetAllAsync(td => td.CustomerId == customerId,
                    td => td.Car,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب قيادة العميل {CustomerId}", customerId);
                throw new Exception($"خطأ في جلب تجارب قيادة العميل {customerId}", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetTestDrivesByCarAsync(int carId)
        {
            try
            {
                return await GetAllAsync(td => td.CarId == carId,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب قيادة السيارة {CarId}", carId);
                throw new Exception($"خطأ في جلب تجارب قيادة السيارة {carId}", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetTestDrivesByEmployeeAsync(int employeeId)
        {
            try
            {
                return await GetAllAsync(td => td.EmployeeId == employeeId,
                    td => td.Car,
                    td => td.Customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب قيادة الموظف {EmployeeId}", employeeId);
                throw new Exception($"خطأ في جلب تجارب قيادة الموظف {employeeId}", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetTestDrivesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await GetAllAsync(td => td.ScheduledDateTime >= startDate && td.ScheduledDateTime <= endDate,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة في الفترة من {StartDate} إلى {EndDate}", startDate, endDate);
                throw new Exception("خطأ في جلب تجارب القيادة في الفترة المحددة", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetScheduledTestDrivesAsync()
        {
            try
            {
                return await GetAllAsync(td => td.Status == "Scheduled" && td.ScheduledDateTime > DateTime.Now,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة المجدولة");
                throw new Exception("خطأ في جلب تجارب القيادة المجدولة", ex);
            }
        }

        public async Task<bool> IsCarAvailableForTestDriveAsync(int carId, DateTime scheduledDateTime, int durationMinutes)
        {
            try
            {
                var endTime = scheduledDateTime.AddMinutes(durationMinutes);

                var conflictingTestDrives = await _dbSet
                    .Where(td => td.CarId == carId &&
                                (td.Status == "Scheduled" || td.Status == "InProgress") &&
                                ((td.ScheduledDateTime < endTime &&
                                  td.ScheduledDateTime.AddMinutes(td.DurationMinutes) > scheduledDateTime)))
                    .ToListAsync();

                bool isAvailable = !conflictingTestDrives.Any();

                _logger.LogInformation("فحص توفر السيارة {CarId} للتجربة في {DateTime}: {IsAvailable}",
                    carId, scheduledDateTime, isAvailable);

                return isAvailable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في فحص توفر السيارة {CarId} لتجربة القيادة", carId);
                throw new Exception($"خطأ في فحص توفر السيارة {carId} لتجربة القيادة", ex);
            }
        }

        public async Task<TestDrive?> GetTestDriveWithDetailsAsync(int testDriveId)
        {
            try
            {
                return await GetByIdAsync(testDriveId,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee,
                    td => td.Sale!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تفاصيل تجربة القيادة {TestDriveId}", testDriveId);
                throw new Exception($"خطأ في جلب تفاصيل تجربة القيادة {testDriveId}", ex);
            }
        }

        // Additional helper methods for better functionality

        public async Task<IEnumerable<TestDrive>> GetTodaysTestDrivesAsync()
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                return await GetAllAsync(td => td.ScheduledDateTime >= today &&
                                              td.ScheduledDateTime < tomorrow &&
                                              (td.Status == "Scheduled" || td.Status == "InProgress"),
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة اليوم");
                throw new Exception("خطأ في جلب تجارب القيادة اليوم", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetOverdueTestDrivesAsync()
        {
            try
            {
                return await GetAllAsync(td => td.Status == "Scheduled" &&
                                              td.ScheduledDateTime < DateTime.Now,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة المتأخرة");
                throw new Exception("خطأ في جلب تجارب القيادة المتأخرة", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetCompletedTestDrivesAsync()
        {
            try
            {
                return await GetAllAsync(td => td.Status == "Completed",
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة المكتملة");
                throw new Exception("خطأ في جلب تجارب القيادة المكتملة", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetConvertedTestDrivesAsync()
        {
            try
            {
                return await GetAllAsync(td => td.IsConvertedToSale == true,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee,
                    td => td.Sale!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة المحولة لمبيعات");
                throw new Exception("خطأ في جلب تجارب القيادة المحولة لمبيعات", ex);
            }
        }

        public async Task<decimal> GetConversionRateAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var totalTestDrives = await CountAsync(td => td.ScheduledDateTime >= startDate &&
                                                           td.ScheduledDateTime <= endDate &&
                                                           td.Status == "Completed");

                var convertedTestDrives = await CountAsync(td => td.ScheduledDateTime >= startDate &&
                                                               td.ScheduledDateTime <= endDate &&
                                                               td.IsConvertedToSale == true);

                if (totalTestDrives == 0) return 0;

                return Math.Round((decimal)convertedTestDrives / totalTestDrives * 100, 2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حساب معدل التحويل في الفترة من {StartDate} إلى {EndDate}", startDate, endDate);
                throw new Exception("خطأ في حساب معدل التحويل", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetTestDrivesRequiringFollowUpAsync()
        {
            try
            {
                return await GetAllAsync(td => td.RequiresFollowUp == true &&
                                              td.Status == "Completed" &&
                                              td.IsConvertedToSale == false,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة التي تحتاج متابعة");
                throw new Exception("خطأ في جلب تجارب القيادة التي تحتاج متابعة", ex);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetTestDrivesWithIncidentsAsync()
        {
            try
            {
                return await GetAllAsync(td => td.HasIncident == true,
                    td => td.Car,
                    td => td.Customer,
                    td => td.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب تجارب القيادة التي حدث بها حوادث");
                throw new Exception("خطأ في جلب تجارب القيادة التي حدث بها حوادث", ex);
            }
        }

        public async Task StartTestDriveAsync(int testDriveId)
        {
            try
            {
                var testDrive = await GetByIdAsync(testDriveId);
                if (testDrive != null && testDrive.Status == "Scheduled")
                {
                    testDrive.Status = "InProgress";
                    testDrive.ActualStartTime = DateTime.Now;
                    testDrive.UpdatedDate = DateTime.Now;
                    Update(testDrive);
                    _logger.LogInformation("تم بدء تجربة القيادة {TestDriveId}", testDriveId);
                }
                else
                {
                    _logger.LogWarning("لا يمكن بدء تجربة القيادة {TestDriveId} - الحالة الحالية: {Status}",
                        testDriveId, testDrive?.Status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في بدء تجربة القيادة {TestDriveId}", testDriveId);
                throw new Exception($"خطأ في بدء تجربة القيادة {testDriveId}", ex);
            }
        }

        public async Task CompleteTestDriveAsync(int testDriveId, int? mileageAfter = null, int? fuelLevelAfter = null)
        {
            try
            {
                var testDrive = await GetByIdAsync(testDriveId);
                if (testDrive != null && testDrive.Status == "InProgress")
                {
                    testDrive.Status = "Completed";
                    testDrive.ActualEndTime = DateTime.Now;
                    testDrive.UpdatedDate = DateTime.Now;

                    if (mileageAfter.HasValue)
                        testDrive.MileageAfter = mileageAfter.Value;

                    if (fuelLevelAfter.HasValue)
                        testDrive.FuelLevelAfter = fuelLevelAfter.Value;

                    if (testDrive.MileageBefore.HasValue && testDrive.MileageAfter.HasValue)
                    {
                        testDrive.DistanceCovered = testDrive.MileageAfter.Value - testDrive.MileageBefore.Value;
                    }

                    Update(testDrive);
                    _logger.LogInformation("تم إنهاء تجربة القيادة {TestDriveId}", testDriveId);
                }
                else
                {
                    _logger.LogWarning("لا يمكن إنهاء تجربة القيادة {TestDriveId} - الحالة الحالية: {Status}",
                        testDriveId, testDrive?.Status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في إنهاء تجربة القيادة {TestDriveId}", testDriveId);
                throw new Exception($"خطأ في إنهاء تجربة القيادة {testDriveId}", ex);
            }
        }

        public async Task CancelTestDriveAsync(int testDriveId, string cancellationReason)
        {
            try
            {
                var testDrive = await GetByIdAsync(testDriveId);
                if (testDrive != null && (testDrive.Status == "Scheduled" || testDrive.Status == "InProgress"))
                {
                    testDrive.Status = "Cancelled";
                    testDrive.CancellationReason = cancellationReason;
                    testDrive.CancellationDate = DateTime.Now;
                    testDrive.UpdatedDate = DateTime.Now;
                    Update(testDrive);
                    _logger.LogInformation("تم إلغاء تجربة القيادة {TestDriveId} بسبب: {Reason}", testDriveId, cancellationReason);
                }
                else
                {
                    _logger.LogWarning("لا يمكن إلغاء تجربة القيادة {TestDriveId} - الحالة الحالية: {Status}",
                        testDriveId, testDrive?.Status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في إلغاء تجربة القيادة {TestDriveId}", testDriveId);
                throw new Exception($"خطأ في إلغاء تجربة القيادة {testDriveId}", ex);
            }
        }

        public async Task RescheduleTestDriveAsync(int testDriveId, DateTime newDateTime, string rescheduleReason)
        {
            try
            {
                var testDrive = await GetByIdAsync(testDriveId);
                if (testDrive != null && testDrive.Status == "Scheduled")
                {
           
                    var isAvailable = await IsCarAvailableForTestDriveAsync(testDrive.CarId, newDateTime, testDrive.DurationMinutes);

                    if (isAvailable)
                    {
                        testDrive.ScheduledDateTime = newDateTime;
                        testDrive.Status = "Rescheduled";
                        testDrive.RescheduleDate = DateTime.Now;
                        testDrive.RescheduleReason = rescheduleReason;
                        testDrive.UpdatedDate = DateTime.Now;
                        Update(testDrive);
                        _logger.LogInformation("تم تغيير موعد تجربة القيادة {TestDriveId} إلى {NewDateTime}",
                            testDriveId, newDateTime);
                    }
                    else
                    {
                        _logger.LogWarning("الموعد الجديد {NewDateTime} غير متاح للسيارة {CarId}",
                            newDateTime, testDrive.CarId);
                        throw new Exception("الموعد الجديد غير متاح");
                    }
                }
                else
                {
                    _logger.LogWarning("لا يمكن تغيير موعد تجربة القيادة {TestDriveId} - الحالة الحالية: {Status}",
                        testDriveId, testDrive?.Status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في تغيير موعد تجربة القيادة {TestDriveId}", testDriveId);
                throw new Exception($"خطأ في تغيير موعد تجربة القيادة {testDriveId}", ex);
            }
        }

        public async Task MarkAsConvertedToSaleAsync(int testDriveId, int saleId)
        {
            try
            {
                var testDrive = await GetByIdAsync(testDriveId);
                if (testDrive != null)
                {
                    testDrive.IsConvertedToSale = true;
                    testDrive.SaleId = saleId;
                    testDrive.ConversionDate = DateTime.Now;

                    testDrive.DaysToConversion = (DateTime.Now - testDrive.ScheduledDateTime).Days;


                    testDrive.UpdatedDate = DateTime.Now;
                    Update(testDrive);
                    _logger.LogInformation("تم ربط تجربة القيادة {TestDriveId} بالمبيعة {SaleId}", testDriveId, saleId);
                }
                else
                {
                    _logger.LogWarning("لم يتم العثور على تجربة القيادة {TestDriveId}", testDriveId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في ربط تجربة القيادة {TestDriveId} بالمبيعة", testDriveId);
                throw new Exception($"خطأ في ربط تجربة القيادة {testDriveId} بالمبيعة", ex);
            }
        }
    }
}

