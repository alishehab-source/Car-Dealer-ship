namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeHierarchyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public List<EmployeeHierarchyDto> Subordinates { get; set; } = new List<EmployeeHierarchyDto>();
    }
    public class EmployeeWithSalesDto : EmployeeDto
    {
        public List<EmployeeSaleDto> RecentSales { get; set; } = new List<EmployeeSaleDto>();
    }

}
