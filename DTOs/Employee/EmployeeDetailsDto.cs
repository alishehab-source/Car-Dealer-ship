namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeDetailsDto : EmployeeResponseDto
    {
        public List<EmployeeSaleDto>? SalesHistory { get; set; }
        public List<EmployeeTestDriveDto>? TestDriveHistory { get; set; }
        public List<EmployeePerformanceDto>? PerformanceHistory { get; set; }
        public List<EmployeeSubordinateDto>? Subordinates { get; set; }
        public EmployeeStatisticsDto? Statistics { get; set; }
        public EmployeeScheduleDto? Schedule { get; set; }
    }

}
