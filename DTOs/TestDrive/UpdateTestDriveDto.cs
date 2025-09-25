using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.TestDrive
{
    public class UpdateTestDriveDto
    {
        [Required(ErrorMessage = "معرف تجربة القيادة مطلوب")]
        public int Id { get; set; }

        public DateTime? ScheduledDateTime { get; set; }

        [Range(15, 180, ErrorMessage = "مدة تجربة القيادة يجب أن تكون بين 15 و 180 دقيقة")]
        public int? DurationMinutes { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        [Range(0, 999, ErrorMessage = "المسافة المقطوعة يجب أن تكون رقم موجب")]
        public int? DistanceCovered { get; set; }

        [Range(0, 999999, ErrorMessage = "قراءة العداد قبل التجربة يجب أن تكون رقم موجب")]
        public int? MileageBefore { get; set; }

        [Range(0, 999999, ErrorMessage = "قراءة العداد بعد التجربة يجب أن تكون رقم موجب")]
        public int? MileageAfter { get; set; }

        [Range(0, 100, ErrorMessage = "مستوى الوقود قبل التجربة يجب أن يكون بين 0 و 100")]
        public int? FuelLevelBefore { get; set; }

        [Range(0, 100, ErrorMessage = "مستوى الوقود بعد التجربة يجب أن يكون بين 0 و 100")]
        public int? FuelLevelAfter { get; set; }

        [RegularExpression("^(Scheduled|InProgress|Completed|Cancelled|NoShow|Rescheduled)$", ErrorMessage = "حالة تجربة القيادة غير صحيحة")]
        public string? Status { get; set; }

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

        public bool? IsInterestedInPurchase { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "السعر المقترح يجب أن يكون رقم موجب")]
        public decimal? ProposedPrice { get; set; }

        [StringLength(200, ErrorMessage = "شروط الشراء المقترحة يجب ألا تزيد عن 200 حرف")]
        public string? ProposedTerms { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [StringLength(500, ErrorMessage = "ملاحظات المتابعة يجب ألا تزيد عن 500 حرف")]
        public string? FollowUpNotes { get; set; }

        public bool? HasIncident { get; set; }

        [StringLength(1000, ErrorMessage = "تفاصيل الحادث يجب ألا تزيد عن 1000 حرف")]
        public string? IncidentDetails { get; set; }

        [StringLength(200, ErrorMessage = "نوع الحادث يجب ألا يزيد عن 200 حرف")]
        public string? IncidentType { get; set; }

        [Range(0, 999999.99, ErrorMessage = "تكلفة الإصلاح يجب أن تكون رقم موجب")]
        public decimal? RepairCost { get; set; }

        [StringLength(20, ErrorMessage = "حالة الطقس يجب ألا تزيد عن 20 حرف")]
        public string? WeatherCondition { get; set; }

        [StringLength(20, ErrorMessage = "حالة الطريق يجب ألا تزيد عن 20 حرف")]
        public string? RoadCondition { get; set; }

        [StringLength(20, ErrorMessage = "كثافة المرور يجب ألا تزيد عن 20 حرف")]
        public string? TrafficCondition { get; set; }

        [Range(-50, 60, ErrorMessage = "درجة الحرارة يجب أن تكون بين -50 و 60")]
        public int? Temperature { get; set; }

    }
}