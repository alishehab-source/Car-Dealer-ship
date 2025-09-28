using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Customer;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<CustomerDto>>>> GetAllCustomers([FromQuery] CustomerSearchDto searchDto)
        {
            try
            {
                var result = await _customerService.GetAllCustomersAsync(searchDto);
                return Ok(ApiResponse<PagedResult<CustomerDto>>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers");
                return StatusCode(500, ApiResponse<PagedResult<CustomerDto>>.ErrorResponse("Error retrieving customers"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                    return NotFound(ApiResponse<CustomerDto>.ErrorResponse("Customer not found"));

                return Ok(ApiResponse<CustomerDto>.SuccessResponse(customer));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer {Id}", id);
                return StatusCode(500, ApiResponse<CustomerDto>.ErrorResponse("Error retrieving customer"));
            }
        }
    }
}
