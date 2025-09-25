using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم العميل مطلوب")]
        [StringLength(100, ErrorMessage = "اسم العميل يجب ألا يزيد عن 100 حرف")]
        [MinLength(2, ErrorMessage = "اسم العميل يجب ألا يقل عن حرفين")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(20, ErrorMessage = "رقم الهاتف يجب ألا يزيد عن 20 حرف")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [StringLength(100, ErrorMessage = "البريد الإلكتروني يجب ألا يزيد عن 100 حرف")]
        public string? Email { get; set; }

        [StringLength(200, ErrorMessage = "العنوان يجب ألا يزيد عن 200 حرف")]
        public string? Address { get; set; }

        [StringLength(50, ErrorMessage = "المدينة يجب ألا تزيد عن 50 حرف")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "المحافظة يجب ألا تزيد عن 50 حرف")]
        public string? State { get; set; }

        [StringLength(10, ErrorMessage = "الرمز البريدي يجب ألا يزيد عن 10 أرقام")]
        [RegularExpression(@"^[0-9]{5,10}$", ErrorMessage = "الرمز البريدي يجب أن يكون أرقام فقط")]
        public string? ZipCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10, ErrorMessage = "الجنس يجب ألا يزيد عن 10 أحرف")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "الجنس يجب أن يكون Male أو Female أو Other")]
        public string? Gender { get; set; }

        [StringLength(20, ErrorMessage = "الحالة الاجتماعية يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Single|Married|Divorced|Widowed)$", ErrorMessage = "الحالة الاجتماعية يجب أن تكون Single أو Married أو Divorced أو Widowed")]
        public string? MaritalStatus { get; set; }

        [StringLength(100, ErrorMessage = "المهنة يجب ألا تزيد عن 100 حرف")]
        public string? Occupation { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99, ErrorMessage = "الراتب يجب أن يكون رقم موجب")]
        public decimal? MonthlyIncome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99, ErrorMessage = "الميزانية يجب أن تكون رقم موجب")]
        public decimal Budget { get; set; }

        [StringLength(50, ErrorMessage = "الماركة المفضلة يجب ألا تزيد عن 50 حرف")]
        public string? PreferredMake { get; set; }

        [StringLength(50, ErrorMessage = "الموديل المفضل يجب ألا يزيد عن 50 حرف")]
        public string? PreferredModel { get; set; }

        [Range(1980, 2030, ErrorMessage = "السنة المفضلة يجب أن تكون بين 1980 و 2030")]
        public int? PreferredYear { get; set; }

        [StringLength(30, ErrorMessage = "اللون المفضل يجب ألا يزيد عن 30 حرف")]
        public string? PreferredColor { get; set; }

        [StringLength(20, ErrorMessage = "نوع الوقود المفضل يجب ألا يزيد عن 20 حرف")]
        [RegularExpression("^(Gasoline|Diesel|Hybrid|Electric|CNG|Any)$", ErrorMessage = "نوع الوقود المفضل يجب أن يكون Gasoline أو Diesel أو Hybrid أو Electric أو CNG أو Any")]
        public string? PreferredFuelType { get; set; }

        [StringLength(20, ErrorMessage = "ناقل الحركة المفضل يجب ألا يزيد عن 20 حرف")]
        [RegularExpression("^(Manual|Automatic|CVT|Any)$", ErrorMessage = "ناقل الحركة المفضل يجب أن يكون Manual أو Automatic أو CVT أو Any")]
        public string? PreferredTransmission { get; set; }

        [Required(ErrorMessage = "حالة العميل مطلوبة")]
        [StringLength(20, ErrorMessage = "حالة العميل يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Lead|Prospect|Hot|Cold|Customer|Lost)$", ErrorMessage = "حالة العميل يجب أن تكون Lead أو Prospect أو Hot أو Cold أو Customer أو Lost")]
        public string Status { get; set; } = "Lead";

        [StringLength(20, ErrorMessage = "مصدر العميل يجب ألا يزيد عن 20 حرف")]
        [RegularExpression("^(Website|Referral|Advertisement|SocialMedia|WalkIn|Phone|Other)$", ErrorMessage = "مصدر العميل يجب أن يكون Website أو Referral أو Advertisement أو SocialMedia أو WalkIn أو Phone أو Other")]
        public string? Source { get; set; }

        [StringLength(100, ErrorMessage = "تفاصيل المصدر يجب ألا تزيد عن 100 حرف")]
        public string? SourceDetails { get; set; }

        public DateTime? LastContactDate { get; set; }

        public DateTime? NextContactDate { get; set; }

        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تزيد عن 1000 حرف")]
        public string? Notes { get; set; }

        [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
        public int? Rating { get; set; } // Customer satisfaction rating

        public bool HasDriverLicense { get; set; } = true;

        [StringLength(20, ErrorMessage = "رقم الرخصة يجب ألا يزيد عن 20 حرف")]
        public string? DriverLicenseNumber { get; set; }

        public DateTime? DriverLicenseExpiryDate { get; set; }

        [StringLength(100, ErrorMessage = "بيانات الاتصال الطارئ يجب ألا تزيد عن 100 حرف")]
        public string? EmergencyContact { get; set; }

        [StringLength(20, ErrorMessage = "رقم الاتصال الطارئ يجب ألا يزيد عن 20 حرف")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الاتصال الطارئ غير صحيح")]
        public string? EmergencyPhone { get; set; }

        public bool OptInForMarketing { get; set; } = true;

        public bool OptInForSMS { get; set; } = true;

        public bool OptInForEmail { get; set; } = true;

        [StringLength(10, ErrorMessage = "اللغة المفضلة يجب ألا تزيد عن 10 أحرف")]
        [RegularExpression("^(Arabic|English|French|Other)$", ErrorMessage = "اللغة المفضلة يجب أن تكون Arabic أو English أو French أو Other")]
        public string PreferredLanguage { get; set; } = "Arabic";

        [StringLength(20, ErrorMessage = "طريقة الاتصال المفضلة يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Phone|Email|SMS|WhatsApp|InPerson)$", ErrorMessage = "طريقة الاتصال المفضلة يجب أن تكون Phone أو Email أو SMS أو WhatsApp أو InPerson")]
        public string PreferredContactMethod { get; set; } = "Phone";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public DateTime? LastPurchaseDate { get; set; }

        [Range(0, 999, ErrorMessage = "عدد المشتريات يجب أن يكون رقم موجب")]
        public int TotalPurchases { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99, ErrorMessage = "إجمالي قيمة المشتريات يجب أن تكون رقم موجب")]
        public decimal TotalPurchaseValue { get; set; } = 0;

        // Navigation Properties
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<TestDrive> TestDrives { get; set; } = new List<TestDrive>();

        // Customer Inquiries or Follow-ups (إضافة مستقبلية)
        // public ICollection<CustomerInquiry> Inquiries { get; set; } = new List<CustomerInquiry>();
        // public ICollection<CustomerFollowUp> FollowUps { get; set; } = new List<CustomerFollowUp>();
    }
}
