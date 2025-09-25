using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        [Required(ErrorMessage = "معرف العميل مطلوب")]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "اسم العميل يجب أن يكون بين 2 و 100 حرف")]
        public string? Name { get; set; }

        [StringLength(20, ErrorMessage = "رقم الهاتف يجب ألا يزيد عن 20 حرف")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string? Phone { get; set; }

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
        public string? ZipCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "الجنس غير صحيح")]
        public string? Gender { get; set; }

        [RegularExpression("^(Single|Married|Divorced|Widowed)$", ErrorMessage = "الحالة الاجتماعية غير صحيحة")]
        public string? MaritalStatus { get; set; }

        [StringLength(100, ErrorMessage = "المهنة يجب ألا تزيد عن 100 حرف")]
        public string? Occupation { get; set; }

        [Range(0, 99999999.99, ErrorMessage = "الراتب يجب أن يكون رقم موجب")]
        public decimal? MonthlyIncome { get; set; }

        [Range(0.01, 99999999.99, ErrorMessage = "الميزانية يجب أن تكون أكبر من صفر")]
        public decimal? Budget { get; set; }

        [RegularExpression("^(Lead|Prospect|Hot|Cold|Customer|Lost)$", ErrorMessage = "حالة العميل غير صحيحة")]
        public string? Status { get; set; }

        [StringLength(50, ErrorMessage = "الماركة المفضلة يجب ألا تزيد عن 50 حرف")]
        public string? PreferredMake { get; set; }

        [StringLength(50, ErrorMessage = "الموديل المفضل يجب ألا يزيد عن 50 حرف")]
        public string? PreferredModel { get; set; }

        [Range(1980, 2030, ErrorMessage = "السنة المفضلة يجب أن تكون بين 1980 و 2030")]
        public int? PreferredYear { get; set; }

        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تزيد عن 1000 حرف")]
        public string? Notes { get; set; }

        public DateTime? NextContactDate { get; set; }

        [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
        public int? Rating { get; set; }

        public bool? OptInForMarketing { get; set; }
        public bool? OptInForSMS { get; set; }
        public bool? OptInForEmail { get; set; }

        [RegularExpression("^(Arabic|English|French|Other)$", ErrorMessage = "اللغة المفضلة غير صحيحة")]
        public string? PreferredLanguage { get; set; }

        [RegularExpression("^(Phone|Email|SMS|WhatsApp|InPerson)$", ErrorMessage = "طريقة الاتصال المفضلة غير صحيحة")]
        public string? PreferredContactMethod { get; set; }
    }

}
