using CarDealershipAPI.DTOs.Car;
using CarDealershipAPI.DTOs.common;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        /// <param name="searchDto">Search and filter parameters</param>

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<CarDto>>>> GetAllCars([FromQuery] CarSearchDto searchDto)
        {
            try
            {
                var result = await _carService.GetAllCarsAsync(searchDto);
                return Ok(ApiResponse<PagedResult<CarDto>>.SuccessResponse(result, "Cars retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cars");
                return StatusCode(500, ApiResponse<PagedResult<CarDto>>.ErrorResponse("An error occurred while retrieving cars"));
            }
        }

  
        /// <param name="id">Car ID</param>

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CarDto>>> GetCarById(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                if (car == null)
                {
                    return NotFound(ApiResponse<CarDto>.ErrorResponse("Car not found"));
                }

                return Ok(ApiResponse<CarDto>.SuccessResponse(car, "Car retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving car with ID: {CarId}", id);
                return StatusCode(500, ApiResponse<CarDto>.ErrorResponse("An error occurred while retrieving the car"));
            }
        }

        /// <param name="createCarDto">Car data</param>

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CarDto>>> CreateCar([FromBody] CreateCarDto createCarDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<CarDto>.ErrorResponse("Validation failed", errors));
            }

            try
            {
                var car = await _carService.CreateCarAsync(createCarDto);
                return CreatedAtAction(
                    nameof(GetCarById),
                    new { id = car.Id },
                    ApiResponse<CarDto>.SuccessResponse(car, "Car created successfully")
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating car");
                return StatusCode(500, ApiResponse<CarDto>.ErrorResponse("An error occurred while creating the car"));
            }
        }

        /// <param name="id">Car ID</param>
        /// <param name="updateCarDto">Updated car data</param>

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CarDto>>> UpdateCar(int id, [FromBody] UpdateCarDto updateCarDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<CarDto>.ErrorResponse("Validation failed", errors));
            }

            try
            {
                var car = await _carService.UpdateCarAsync(id, updateCarDto);
                if (car == null)
                {
                    return NotFound(ApiResponse<CarDto>.ErrorResponse("Car not found"));
                }

                return Ok(ApiResponse<CarDto>.SuccessResponse(car, "Car updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating car with ID: {CarId}", id);
                return StatusCode(500, ApiResponse<CarDto>.ErrorResponse("An error occurred while updating the car"));
            }
        }

        /// <param name="id">Car ID</param>

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteCar(int id)
        {
            try
            {
                var success = await _carService.DeleteCarAsync(id);
                if (!success)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Car not found or cannot be deleted"));
                }

                return Ok(ApiResponse<object>.SuccessResponse(null, "Car deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car with ID: {CarId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while deleting the car"));
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<ApiResponse<List<CarSummaryDto>>>> GetAvailableCars()
        {
            try
            {
                var cars = await _carService.GetAvailableCarsAsync();
                return Ok(ApiResponse<List<CarSummaryDto>>.SuccessResponse(cars, "Available cars retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving available cars");
                return StatusCode(500, ApiResponse<List<CarSummaryDto>>.ErrorResponse("An error occurred while retrieving available cars"));
            }
        }

        [HttpGet("{id}/availability")]
        public async Task<ActionResult<ApiResponse<bool>>> CheckCarAvailability(int id)
        {
            try
            {
                var isAvailable = await _carService.IsCarAvailableAsync(id);
                return Ok(ApiResponse<bool>.SuccessResponse(isAvailable, "Car availability checked successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking availability for car with ID: {CarId}", id);
                return StatusCode(500, ApiResponse<bool>.ErrorResponse("An error occurred while checking car availability"));
            }
        }

        [HttpPatch("{id}/mark-sold")]
        public async Task<ActionResult<ApiResponse<object>>> MarkCarAsSold(int id)
        {
            try
            {
                var success = await _carService.MarkCarAsSoldAsync(id);
                if (!success)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Car not found or not available"));
                }

                return Ok(ApiResponse<object>.SuccessResponse(null, "Car marked as sold successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking car as sold with ID: {CarId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while marking the car as sold"));
            }
        }

        [HttpPatch("{id}/mark-reserved")]
        public async Task<ActionResult<ApiResponse<object>>> MarkCarAsReserved(int id)
        {
            try
            {
                var success = await _carService.MarkCarAsReservedAsync(id);
                if (!success)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Car not found or not available"));
                }

                return Ok(ApiResponse<object>.SuccessResponse(null, "Car marked as reserved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking car as reserved with ID: {CarId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while marking the car as reserved"));
            }
        }

        [HttpGet("filter-options")]
        public async Task<ActionResult<ApiResponse<FilterOptionsDto>>> GetFilterOptions()
        {
            try
            {
                var options = await _carService.GetFilterOptionsAsync();
                return Ok(ApiResponse<FilterOptionsDto>.SuccessResponse(options, "Filter options retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving filter options");
                return StatusCode(500, ApiResponse<FilterOptionsDto>.ErrorResponse("An error occurred while retrieving filter options"));
            }
        }
    }

}
