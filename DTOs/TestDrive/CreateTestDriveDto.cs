using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.TestDrive
{
    public class CreateTestDriveDto
    {
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

        [StringLength(200, ErrorMessage = "نقطة البداية يجب ألا تزيد عن 200 حرف")]
        public string? StartLocation { get; set; } = "المعرض";

        [StringLength(500, ErrorMessage = "الطريق المخطط يجب ألا يزيد عن 500 حرف")]
        public string? PlannedRoute { get; set; }

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

        [StringLength(20, ErrorMessage = "حالة الطقس يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Sunny|Cloudy|Rainy|Stormy|Foggy|Snow|Windy)$", ErrorMessage = "حالة الطقس غير صحيحة")]
        public string? WeatherCondition { get; set; }

        [StringLength(50, ErrorMessage = "نوع الطريق يجب ألا يزيد عن 50 حرف")]
        [RegularExpression("^(City|Highway|Mixed|OffRoad|Parking)$", ErrorMessage = "نوع الطريق غير صحيح")]
        public string? DriveType { get; set; } = "Mixed";

        [StringLength(1000, ErrorMessage = "ملاحظات الموظف يجب ألا تزيد عن 1000 حرف")]
        public string? EmployeeNotes { get; set; }

        [StringLength(20, ErrorMessage = "أولوية المتابعة يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Low|Medium|High|Urgent)$", ErrorMessage = "أولوية المتابعة غير صحيحة")]
        public string FollowUpPriority { get; set; } = "Medium";

        public bool RequiresFollowUp { get; set; } = true;
    }


}
