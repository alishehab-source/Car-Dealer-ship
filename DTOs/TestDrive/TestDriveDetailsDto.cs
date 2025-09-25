using CarDealershipAPI.DTO.Car;
using CarDealershipAPI.DTOs.Customer;
using CarDealershipAPI.DTOs.Employee;

namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveDetailsDto : TestDriveResponseDto
    {
        public CarDetailsDto? Car { get; set; }
        public CustomerDetailsDto? Customer { get; set; }
        public EmployeeDetailsDto? Employee { get; set; }
        public SaleDetailsDto? Sale { get; set; }
        public List<TestDriveChecklistItemDto>? ChecklistItems { get; set; }
        public List<TestDrivePhotoDto>? Photos { get; set; }
        public List<TestDriveActivityDto>? ActivityLog { get; set; }
    }

}
