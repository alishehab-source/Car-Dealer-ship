using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.DTOs.Employee;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<EmployeeDto>>>> GetAllEmployees([FromQuery] EmployeeSearchDto searchDto)
        {
            try
            {
                var result = await _employeeService.GetAllEmployeesAsync(searchDto);
                return Ok(ApiResponse<PagedResult<EmployeeDto>>.SuccessResponse(result, "Employees retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving employees");
                return StatusCode(500, ApiResponse<PagedResult<EmployeeDto>>.ErrorResponse("An error occurred while retrieving employees"));
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound(ApiResponse<EmployeeDto>.ErrorResponse("Employee not found"));
                }

                return Ok(ApiResponse<EmployeeDto>.SuccessResponse(employee, "Employee retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving employee with ID: {EmployeeId}", id);
                return StatusCode(500, ApiResponse<EmployeeDto>.ErrorResponse("An error occurred while retrieving the employee"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<EmployeeDto>.ErrorResponse("Validation failed", errors));
            }

            try
            {
                var employee = await _employeeService.CreateEmployeeAsync(createEmployeeDto);
                return CreatedAtAction(
                    nameof(GetEmployeeById),
                    new { id = employee.Id },
                    ApiResponse<EmployeeDto>.SuccessResponse(employee, "Employee created successfully")
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                return StatusCode(500, ApiResponse<EmployeeDto>.ErrorResponse("An error occurred while creating the employee"));
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<EmployeeDto>.ErrorResponse("Validation failed", errors));
            }

            try
            {
                var employee = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
                if (employee == null)
                {
                    return NotFound(ApiResponse<EmployeeDto>.ErrorResponse("Employee not found"));
                }

                return Ok(ApiResponse<EmployeeDto>.SuccessResponse(employee, "Employee updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee with ID: {EmployeeId}", id);
                return StatusCode(500, ApiResponse<EmployeeDto>.ErrorResponse("An error occurred while updating the employee"));
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteEmployee(int id)
        {
            try
            {
                var success = await _employeeService.DeleteEmployeeAsync(id);
                if (!success)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Employee not found or cannot be deleted"));
                }

                return Ok(ApiResponse<object>.SuccessResponse(null, "Employee deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee with ID: {EmployeeId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while deleting the employee"));
            }
        }


        [HttpGet("{id}/sales")]
        public async Task<ActionResult<ApiResponse<EmployeeWithSalesDto>>> GetEmployeeWithSales(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeWithSalesAsync(id);
                if (employee == null)
                {
                    return NotFound(ApiResponse<EmployeeWithSalesDto>.ErrorResponse("Employee not found"));
                }

                return Ok(ApiResponse<EmployeeWithSalesDto>.SuccessResponse(employee, "Employee with sales retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving employee with sales for ID: {EmployeeId}", id);
                return StatusCode(500, ApiResponse<EmployeeWithSalesDto>.ErrorResponse("An error occurred while retrieving employee sales"));
            }
        }

       
        [HttpGet("top-performers")]
        public async Task<ActionResult<ApiResponse<List<EmployeePerformanceDto>>>> GetTopPerformers([FromQuery] int count = 10)
        {
            try
            {
                var performers = await _employeeService.GetTopPerformersAsync(count);
                return Ok(ApiResponse<List<EmployeePerformanceDto>>.SuccessResponse(performers, "Top performers retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top performers");
                return StatusCode(500, ApiResponse<List<EmployeePerformanceDto>>.ErrorResponse("An error occurred while retrieving top performers"));
            }
        }

        [HttpGet("{id}/hierarchy")]
        public async Task<ActionResult<ApiResponse<EmployeeHierarchyDto>>> GetEmployeeHierarchy(int id)
        {
            try
            {
                var hierarchy = await _employeeService.GetEmployeeHierarchyAsync(id);
                if (hierarchy == null)
                {
                    return NotFound(ApiResponse<EmployeeHierarchyDto>.ErrorResponse("Employee not found"));
                }

                return Ok(ApiResponse<EmployeeHierarchyDto>.SuccessResponse(hierarchy, "Employee hierarchy retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving employee hierarchy for ID: {EmployeeId}", id);
                return StatusCode(500, ApiResponse<EmployeeHierarchyDto>.ErrorResponse("An error occurred while retrieving employee hierarchy"));
            }
        }


        [HttpGet("active")]
        public async Task<ActionResult<ApiResponse<List<EmployeeSummaryDto>>>> GetActiveEmployees()
        {
            try
            {
                var employees = await _employeeService.GetActiveEmployeesAsync();
                return Ok(ApiResponse<List<EmployeeSummaryDto>>.SuccessResponse(employees, "Active employees retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active employees");
                return StatusCode(500, ApiResponse<List<EmployeeSummaryDto>>.ErrorResponse("An error occurred while retrieving active employees"));
            }
        }

        [HttpPost("{id}/update-performance")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateEmployeePerformance(int id)
        {
            try
            {
                var success = await _employeeService.UpdateEmployeePerformanceAsync(id);
                if (!success)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Employee not found"));
                }

                return Ok(ApiResponse<object>.SuccessResponse(null, "Employee performance updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating performance for employee with ID: {EmployeeId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while updating employee performance"));
            }
        }
    }

}
