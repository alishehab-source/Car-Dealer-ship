using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.TestDrive
{

    public class TestDriveCompleteDto
    {
        [Required(ErrorMessage = "معرف تجربة القيادة مطلوب")]
        public int TestDriveId { get; set; }

        [Range(0, 999999, ErrorMessage = "قراءة العداد بعد التجربة يجب أن تكون رقم موجب")]
        public int? MileageAfter { get; set; }

        [Range(0, 100, ErrorMessage = "مستوى الوقود بعد التجربة يجب أن يكون بين 0 و 100")]
        public int? FuelLevelAfter { get; set; }

        [StringLength(500, ErrorMessage = "الطريق الفعلي يجب ألا يزيد عن 500 حرف")]
        public string? ActualRoute { get; set; }

        [Range(1, 5, ErrorMessage = "تقييم العميل للسيارة يجب أن يكون بين 1 و 5")]
        public int? CustomerRating { get; set; }

        [Range(1, 5, ErrorMessage = "تقييم العميل للخدمة يجب أن يكون بين 1 و 5")]
        public int? ServiceRating { get; set; }

        [StringLength(1000, ErrorMessage = "تعليقات العميل يجب ألا تزيد عن 1000 حرف")]
        public string? CustomerFeedback { get; set; }

        [StringLength(1000, ErrorMessage = "ملاحظات الموظف يجب ألا تزيد عن 1000 حرف")]
        public string? EmployeeNotes { get; set; }

        public bool IsInterestedInPurchase { get; set; } = false;

        [Range(0, 9999999.99, ErrorMessage = "السعر المقترح يجب أن يكون رقم موجب")]
        public decimal? ProposedPrice { get; set; }

        [StringLength(200, ErrorMessage = "شروط الشراء المقترحة يجب ألا تزيد عن 200 حرف")]
        public string? ProposedTerms { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public bool HasIncident { get; set; } = false;

        [StringLength(1000, ErrorMessage = "تفاصيل الحادث يجب ألا تزيد عن 1000 حرف")]
        public string? IncidentDetails { get; set; }

        [Range(0, 999999.99, ErrorMessage = "تكلفة الإصلاح يجب أن تكون رقم موجب")]
        public decimal? RepairCost { get; set; }
    }

}