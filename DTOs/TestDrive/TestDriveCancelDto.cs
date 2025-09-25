using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.TestDrive
{
    public class TestDriveCancelDto
    {
        [Required(ErrorMessage = "معرف تجربة القيادة مطلوب")]
        public int TestDriveId { get; set; }

        [Required(ErrorMessage = "سبب الإلغاء مطلوب")]
        [StringLength(200, ErrorMessage = "سبب الإلغاء يجب ألا يزيد عن 200 حرف")]
        public string CancellationReason { get; set; } = string.Empty;
    }


}
