using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class TestDrive
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "معرف السيارة مطلوب")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "معرف العميل مطلوب")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "معرف الموظف مطلوب")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "تاريخ ووقت الحجز مطلوب")]
        public DateTime ScheduledDateTime { get; set; }

        [Required(ErrorMessage = "مدة تجربة القيادة مطلوبة")]
        [Range(15, 180, ErrorMessage = "مدة تجربة القيادة يجب أن تكون بين 15 و 180 دقيقة")]
        public int DurationMinutes { get; set; } = 30;

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        [Range(0, 999, ErrorMessage = "المسافة المقطوعة يجب أن تكون رقم موجب")]
        public int? DistanceCovered { get; set; } // في الكيلومترات

        [Range(0, 999999, ErrorMessage = "قراءة العداد قبل التجربة يجب أن تكون رقم موجب")]
        public int? MileageBefore { get; set; }

        [Range(0, 999999, ErrorMessage = "قراءة العداد بعد التجربة يجب أن تكون رقم موجب")]
        public int? MileageAfter { get; set; }

        [Range(0, 100, ErrorMessage = "مستوى الوقود قبل التجربة يجب أن يكون بين 0 و 100")]
        public int? FuelLevelBefore { get; set; } // نسبة مئوية

        [Range(0, 100, ErrorMessage = "مستوى الوقود بعد التجربة يجب أن يكون بين 0 و 100")]
        public int? FuelLevelAfter { get; set; } // نسبة مئوية

        [Required(ErrorMessage = "حالة تجربة القيادة مطلوبة")]
        [StringLength(20, ErrorMessage = "حالة تجربة القيادة يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Scheduled|InProgress|Completed|Cancelled|NoShow|Rescheduled)$", ErrorMessage = "حالة تجربة القيادة يجب أن تكون Scheduled أو InProgress أو Completed أو Cancelled أو NoShow أو Rescheduled")]
        public string Status { get; set; } = "Scheduled";

        [StringLength(200, ErrorMessage = "سبب الإلغاء يجب ألا يزيد عن 200 حرف")]
        public string? CancellationReason { get; set; }

        public DateTime? CancellationDate { get; set; }

        public DateTime? RescheduleDate { get; set; }

        [StringLength(200, ErrorMessage = "سبب إعادة الجدولة يجب ألا يزيد عن 200 حرف")]
        public string? RescheduleReason { get; set; }

        [StringLength(200, ErrorMessage = "نقطة البداية يجب ألا تزيد عن 200 حرف")]
        public string? StartLocation { get; set; } = "المعرض";

        [StringLength(500, ErrorMessage = "الطريق المخطط يجب ألا يزيد عن 500 حرف")]
        public string? PlannedRoute { get; set; }

        [StringLength(500, ErrorMessage = "الطريق الفعلي يجب ألا يزيد عن 500 حرف")]
        public string? ActualRoute { get; set; }

        [Required(ErrorMessage = "حالة رخصة القيادة مطلوبة")]
        public bool HasValidLicense { get; set; } = true;

        [StringLength(20, ErrorMessage = "رقم رخصة القيادة يجب ألا يزيد عن 20 حرف")]
        public string? LicenseNumber { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        [StringLength(50, ErrorMessage = "جهة إصدار الرخصة يجب ألا تزيد عن 50 حرف")]
        public string? LicenseIssuedBy { get; set; }

        [Required(ErrorMessage = "حالة التأمين مطلوبة")]
        public bool IsInsured { get; set; } = true;

        [StringLength(100, ErrorMessage = "شركة التأمين يجب ألا تزيد عن 100 حرف")]
        public string? InsuranceCompany { get; set; }

        [StringLength(50, ErrorMessage = "رقم بوليصة التأمين يجب ألا يزيد عن 50 حرف")]
        public string? InsurancePolicyNumber { get; set; }

        public DateTime? InsuranceExpiryDate { get; set; }

        [StringLength(100, ErrorMessage = "اسم السائق المرافق يجب ألا يزيد عن 100 حرف")]
        public string? AccompanyingDriver { get; set; }

        [StringLength(20, ErrorMessage = "رقم هاتف السائق المرافق يجب ألا يزيد عن 20 حرف")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم هاتف السائق المرافق غير صحيح")]
        public string? AccompanyingDriverPhone { get; set; }

        [StringLength(100, ErrorMessage = "العلاقة بالسائق المرافق يجب ألا تزيد عن 100 حرف")]
        public string? AccompanyingDriverRelation { get; set; }

        [Range(1, 5, ErrorMessage = "تقييم العميل للسيارة يجب أن يكون بين 1 و 5")]
        public int? CustomerRating { get; set; }

        [Range(1, 5, ErrorMessage = "تقييم العميل للخدمة يجب أن يكون بين 1 و 5")]
        public int? ServiceRating { get; set; }

        [StringLength(1000, ErrorMessage = "تعليقات العميل يجب ألا تزيد عن 1000 حرف")]
        public string? CustomerFeedback { get; set; }

        [StringLength(1000, ErrorMessage = "ملاحظات الموظف يجب ألا تزيد عن 1000 حرف")]
        public string? EmployeeNotes { get; set; }

        public bool IsInterestedInPurchase { get; set; } = false;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "السعر المقترح يجب أن يكون رقم موجب")]
        public decimal? ProposedPrice { get; set; }

        [StringLength(200, ErrorMessage = "شروط الشراء المقترحة يجب ألا تزيد عن 200 حرف")]
        public string? ProposedTerms { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [StringLength(500, ErrorMessage = "ملاحظات المتابعة يجب ألا تزيد عن 500 حرف")]
        public string? FollowUpNotes { get; set; }

        public bool HasIncident { get; set; } = false;

        [StringLength(1000, ErrorMessage = "تفاصيل الحادث يجب ألا تزيد عن 1000 حرف")]
        public string? IncidentDetails { get; set; }

        [StringLength(200, ErrorMessage = "نوع الحادث يجب ألا يزيد عن 200 حرف")]
        public string? IncidentType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999.99, ErrorMessage = "تكلفة الإصلاح يجب أن تكون رقم موجب")]
        public decimal? RepairCost { get; set; }

        public bool IsReported { get; set; } = false;

        [StringLength(50, ErrorMessage = "رقم التقرير يجب ألا يزيد عن 50 حرف")]
        public string? ReportNumber { get; set; }

        [StringLength(100, ErrorMessage = "الجهة المبلغة يجب ألا تزيد عن 100 حرف")]
        public string? ReportedTo { get; set; }

        [StringLength(20, ErrorMessage = "حالة الطقس يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Sunny|Cloudy|Rainy|Stormy|Foggy|Snow|Windy)$", ErrorMessage = "حالة الطقس يجب أن تكون Sunny أو Cloudy أو Rainy أو Stormy أو Foggy أو Snow أو Windy")]
        public string? WeatherCondition { get; set; }

        [StringLength(20, ErrorMessage = "حالة الطريق يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Excellent|Good|Fair|Poor|UnderConstruction)$", ErrorMessage = "حالة الطريق يجب أن تكون Excellent أو Good أو Fair أو Poor أو UnderConstruction")]
        public string? RoadCondition { get; set; }

        [StringLength(20, ErrorMessage = "كثافة المرور يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Light|Moderate|Heavy|VeryHeavy)$", ErrorMessage = "كثافة المرور يجب أن تكون Light أو Moderate أو Heavy أو VeryHeavy")]
        public string? TrafficCondition { get; set; }

        [Range(-50, 60, ErrorMessage = "درجة الحرارة يجب أن تكون بين -50 و 60")]
        public int? Temperature { get; set; } // درجة مئوية

        public bool RequiresFollowUp { get; set; } = true;

        [StringLength(20, ErrorMessage = "أولوية المتابعة يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Low|Medium|High|Urgent)$", ErrorMessage = "أولوية المتابعة يجب أن تكون Low أو Medium أو High أو Urgent")]
        public string FollowUpPriority { get; set; } = "Medium";

        [StringLength(500, ErrorMessage = "الإجراءات المطلوبة يجب ألا تزيد عن 500 حرف")]
        public string? RequiredActions { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999.99, ErrorMessage = "تكلفة الوقود يجب أن تكون رقم موجب")]
        public decimal? FuelCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999.99, ErrorMessage = "تكلفة الصيانة يجب أن تكون رقم موجب")]
        public decimal? MaintenanceCost { get; set; }

        [StringLength(50, ErrorMessage = "نوع الطريق يجب ألا يزيد عن 50 حرف")]
        [RegularExpression("^(City|Highway|Mixed|OffRoad|Parking)$", ErrorMessage = "نوع الطريق يجب أن يكون City أو Highway أو Mixed أو OffRoad أو Parking")]
        public string? DriveType { get; set; } = "Mixed";

        public bool IsConvertedToSale { get; set; } = false;

        public int? SaleId { get; set; }

        public DateTime? ConversionDate { get; set; }

        [Range(1, 30, ErrorMessage = "أيام التحويل للبيع يجب أن تكون بين 1 و 30")]
        public int? DaysToConversion { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        [StringLength(100, ErrorMessage = "المستخدم المنشئ يجب ألا يزيد عن 100 حرف")]
        public string? CreatedBy { get; set; }

        [StringLength(100, ErrorMessage = "المستخدم المحدث يجب ألا يزيد عن 100 حرف")]
        public string? UpdatedBy { get; set; }

        // Navigation Properties
        public Car Car { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public Sale? Sale { get; set; }

        // Additional related entities (إضافة مستقبلية)
        // public ICollection<TestDriveDocument> Documents { get; set; } = new List<TestDriveDocument>();
        // public ICollection<TestDrivePhoto> Photos { get; set; } = new List<TestDrivePhoto>();
        // public ICollection<TestDriveChecklistItem> ChecklistItems { get; set; } = new List<TestDriveChecklistItem>();
    }
}

