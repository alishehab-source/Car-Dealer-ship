using CarDealershipAPI.Data;
using CarDealershipAPI.DTOs.common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly CarDealershipDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(CarDealershipDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<ApiResponse<DashboardStatsDto>>> GetDashboardStats()
        {
            try
            {
                var totalCars = await _context.Cars.CountAsync();
                var availableCars = await _context.Cars.CountAsync(c => c.Status == "Available");
                var totalCustomers = await _context.Customers.CountAsync();
                var totalEmployees = await _context.Employees.CountAsync();
                var totalSales = await _context.Sales.CountAsync(s => s.Status == "Completed");

                var stats = new DashboardStatsDto
                {
                    TotalCars = totalCars,
                    AvailableCars = availableCars,
                    TotalCustomers = totalCustomers,
                    TotalEmployees = totalEmployees,
                    TotalSalesThisMonth = totalSales
                };

                return Ok(ApiResponse<DashboardStatsDto>.SuccessResponse(stats));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dashboard stats");
                return StatusCode(500, ApiResponse<DashboardStatsDto>.ErrorResponse("Error retrieving stats"));
            }
        }
    }
}
