using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveRescheduleDto
    {
        [Required(ErrorMessage = "معرف تجربة القيادة مطلوب")]
        public int TestDriveId { get; set; }

        [Required(ErrorMessage = "التاريخ والوقت الجديد مطلوب")]
        public DateTime NewDateTime { get; set; }

        [Required(ErrorMessage = "سبب إعادة الجدولة مطلوب")]
        [StringLength(200, ErrorMessage = "سبب إعادة الجدولة يجب ألا يزيد عن 200 حرف")]
        public string RescheduleReason { get; set; } = string.Empty;
    }

}
