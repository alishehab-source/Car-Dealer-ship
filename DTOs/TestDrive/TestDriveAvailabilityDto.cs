using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.TestDrive
{

    public class TestDriveAvailabilityDto
    {
        [Required(ErrorMessage = "معرف السيارة مطلوب")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "التاريخ مطلوب")]
        public DateTime Date { get; set; }

        [Range(15, 180, ErrorMessage = "المدة يجب أن تكون بين 15 و 180 دقيقة")]
        public int DurationMinutes { get; set; } = 30;

        public List<AvailableTimeSlotDto>? AvailableSlots { get; set; }
    }

}
