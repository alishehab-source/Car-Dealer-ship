using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Customer
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "اسم العميل مطلوب")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "اسم العميل يجب أن يكون بين 2 و 100 حرف")]
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

        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "الجنس يجب أن يكون Male أو Female أو Other")]
        public string? Gender { get; set; }

        [RegularExpression("^(Single|Married|Divorced|Widowed)$", ErrorMessage = "الحالة الاجتماعية يجب أن تكون Single أو Married أو Divorced أو Widowed")]
        public string? MaritalStatus { get; set; }

        [StringLength(100, ErrorMessage = "المهنة يجب ألا تزيد عن 100 حرف")]
        public string? Occupation { get; set; }

        [Range(0, 99999999.99, ErrorMessage = "الراتب يجب أن يكون رقم موجب")]
        public decimal? MonthlyIncome { get; set; }

        [Required(ErrorMessage = "الميزانية مطلوبة")]
        [Range(0.01, 99999999.99, ErrorMessage = "الميزانية يجب أن تكون أكبر من صفر")]
        public decimal Budget { get; set; }

        [StringLength(50, ErrorMessage = "الماركة المفضلة يجب ألا تزيد عن 50 حرف")]
        public string? PreferredMake { get; set; }

        [StringLength(50, ErrorMessage = "الموديل المفضل يجب ألا يزيد عن 50 حرف")]
        public string? PreferredModel { get; set; }

        [Range(1980, 2030, ErrorMessage = "السنة المفضلة يجب أن تكون بين 1980 و 2030")]
        public int? PreferredYear { get; set; }

        [StringLength(30, ErrorMessage = "اللون المفضل يجب ألا يزيد عن 30 حرف")]
        public string? PreferredColor { get; set; }

        [RegularExpression("^(Gasoline|Diesel|Hybrid|Electric|CNG|Any)$", ErrorMessage = "نوع الوقود المفضل غير صحيح")]
        public string? PreferredFuelType { get; set; }

        [RegularExpression("^(Manual|Automatic|CVT|Any)$", ErrorMessage = "ناقل الحركة المفضل غير صحيح")]
        public string? PreferredTransmission { get; set; }

        [RegularExpression("^(Website|Referral|Advertisement|SocialMedia|WalkIn|Phone|Other)$", ErrorMessage = "مصدر العميل غير صحيح")]
        public string? Source { get; set; }

        [StringLength(100, ErrorMessage = "تفاصيل المصدر يجب ألا تزيد عن 100 حرف")]
        public string? SourceDetails { get; set; }

        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تزيد عن 1000 حرف")]
        public string? Notes { get; set; }

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

        [RegularExpression("^(Arabic|English|French|Other)$", ErrorMessage = "اللغة المفضلة غير صحيحة")]
        public string PreferredLanguage { get; set; } = "Arabic";

        [RegularExpression("^(Phone|Email|SMS|WhatsApp|InPerson)$", ErrorMessage = "طريقة الاتصال المفضلة غير صحيحة")]
        public string PreferredContactMethod { get; set; } = "Phone";
    }

}
