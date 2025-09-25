using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "اسم الموظف مطلوب")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "اسم الموظف يجب أن يكون بين 2 و 100 حرف")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [StringLength(100, ErrorMessage = "البريد الإلكتروني يجب ألا يزيد عن 100 حرف")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(20, ErrorMessage = "رقم الهاتف يجب ألا يزيد عن 20 حرف")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "العنوان يجب ألا يزيد عن 200 حرف")]
        public string? Address { get; set; }

        [StringLength(50, ErrorMessage = "المدينة يجب ألا تزيد عن 50 حرف")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "المحافظة يجب ألا تزيد عن 50 حرف")]
        public string? State { get; set; }

        [StringLength(10, ErrorMessage = "الرمز البريدي يجب ألا يزيد عن 10 أرقام")]
        [RegularExpression(@"^[0-9]{5,10}$", ErrorMessage = "الرمز البريدي يجب أن يكون أرقام فقط")]
        public string? ZipCode { get; set; }

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "الجنس يجب أن يكون Male أو Female أو Other")]
        public string? Gender { get; set; }

        [RegularExpression("^(Single|Married|Divorced|Widowed)$", ErrorMessage = "الحالة الاجتماعية غير صحيحة")]
        public string? MaritalStatus { get; set; }

        [Required(ErrorMessage = "رقم الهوية مطلوب")]
        [StringLength(20, ErrorMessage = "رقم الهوية يجب ألا يزيد عن 20 حرف")]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "نوع الهوية مطلوب")]
        [RegularExpression("^(NationalID|Passport|ResidencePermit)$", ErrorMessage = "نوع الهوية غير صحيح")]
        public string IdentityType { get; set; } = "NationalID";

        [Required(ErrorMessage = "تاريخ التوظيف مطلوب")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "المسمى الوظيفي مطلوب")]
        [RegularExpression("^(Manager|SalesRep|Accountant|Mechanic|Receptionist|Admin|Other)$", ErrorMessage = "المسمى الوظيفي غير صحيح")]
        public string Role { get; set; } = "SalesRep";

        [Required(ErrorMessage = "القسم مطلوب")]
        [RegularExpression("^(Sales|Finance|Service|Administration|Management|IT|HR)$", ErrorMessage = "القسم غير صحيح")]
        public string Department { get; set; } = "Sales";

        [Required(ErrorMessage = "الراتب الأساسي مطلوب")]
        [Range(0.01, 999999.99, ErrorMessage = "الراتب الأساسي يجب أن يكون أكبر من صفر")]
        public decimal BaseSalary { get; set; }

        [Range(0, 1, ErrorMessage = "نسبة العمولة يجب أن تكون بين 0 و 1")]
        public decimal CommissionRate { get; set; } = 0.05m;

        [Range(0, 999999.99, ErrorMessage = "الحد الأقصى للعمولة يجب أن يكون رقم موجب")]
        public decimal? MaxCommission { get; set; }

        [RegularExpression("^(FullTime|PartTime|Contract|Intern|Consultant)$", ErrorMessage = "نوع العمل غير صحيح")]
        public string EmploymentType { get; set; } = "FullTime";

        public int? SupervisorId { get; set; }

        [StringLength(100, ErrorMessage = "المؤهل العلمي يجب ألا يزيد عن 100 حرف")]
        public string? Education { get; set; }

        [StringLength(500, ErrorMessage = "المهارات يجب ألا تزيد عن 500 حرف")]
        public string? Skills { get; set; }

        [StringLength(100, ErrorMessage = "الشهادات يجب ألا تزيد عن 100 حرف")]
        public string? Certifications { get; set; }

        [RegularExpression("^(A\\+|A\\-|B\\+|B\\-|AB\\+|AB\\-|O\\+|O\\-)$", ErrorMessage = "فصيلة الدم غير صحيحة")]
        public string? BloodType { get; set; }

        [StringLength(100, ErrorMessage = "بيانات الاتصال الطارئ يجب ألا تزيد عن 100 حرف")]
        public string? EmergencyContact { get; set; }

        [StringLength(20, ErrorMessage = "رقم الاتصال الطارئ يجب ألا يزيد عن 20 حرف")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الاتصال الطارئ غير صحيح")]
        public string? EmergencyPhone { get; set; }

        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تزيد عن 1000 حرف")]
        public string? Notes { get; set; }

        [Url(ErrorMessage = "رابط الصورة الشخصية غير صحيح")]
        public string? ProfileImageUrl { get; set; }
    }

}
