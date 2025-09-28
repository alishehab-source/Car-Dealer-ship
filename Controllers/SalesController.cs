using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Sale;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISaleService saleService, ILogger<SalesController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<SaleDto>>>> GetAllSales([FromQuery] SaleSearchDto searchDto)
        {
            try
            {
                var result = await _saleService.GetAllSalesAsync(searchDto);
                return Ok(ApiResponse<PagedResult<SaleDto>>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales");
                return StatusCode(500, ApiResponse<PagedResult<SaleDto>>.ErrorResponse("Error retrieving sales"));
            }
        }
    }
}
