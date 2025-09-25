using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        [Required(ErrorMessage = "معرف الموظف مطلوب")]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "اسم الموظف يجب أن يكون بين 2 و 100 حرف")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [StringLength(100, ErrorMessage = "البريد الإلكتروني يجب ألا يزيد عن 100 حرف")]
        public string? Email { get; set; }

        [StringLength(20, ErrorMessage = "رقم الهاتف يجب ألا يزيد عن 20 حرف")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string? Phone { get; set; }

        [StringLength(200, ErrorMessage = "العنوان يجب ألا يزيد عن 200 حرف")]
        public string? Address { get; set; }

        [StringLength(50, ErrorMessage = "المدينة يجب ألا تزيد عن 50 حرف")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "المحافظة يجب ألا تزيد عن 50 حرف")]
        public string? State { get; set; }

        [RegularExpression("^(Manager|SalesRep|Accountant|Mechanic|Receptionist|Admin|Other)$", ErrorMessage = "المسمى الوظيفي غير صحيح")]
        public string? Role { get; set; }

        [RegularExpression("^(Sales|Finance|Service|Administration|Management|IT|HR)$", ErrorMessage = "القسم غير صحيح")]
        public string? Department { get; set; }

        [Range(0.01, 999999.99, ErrorMessage = "الراتب الأساسي يجب أن يكون أكبر من صفر")]
        public decimal? BaseSalary { get; set; }

        [Range(0, 1, ErrorMessage = "نسبة العمولة يجب أن تكون بين 0 و 1")]
        public decimal? CommissionRate { get; set; }

        [Range(0, 999999.99, ErrorMessage = "الحد الأقصى للعمولة يجب أن يكون رقم موجب")]
        public decimal? MaxCommission { get; set; }

        [Range(0, 999.99, ErrorMessage = "البونص يجب أن يكون رقم موجب")]
        public decimal? Bonus { get; set; }

        [RegularExpression("^(Active|Inactive|OnLeave|Terminated|Suspended)$", ErrorMessage = "حالة الموظف غير صحيحة")]
        public string? Status { get; set; }

        [RegularExpression("^(FullTime|PartTime|Contract|Intern|Consultant)$", ErrorMessage = "نوع العمل غير صحيح")]
        public string? EmploymentType { get; set; }

        public int? SupervisorId { get; set; }

        [StringLength(500, ErrorMessage = "المهارات يجب ألا تزيد عن 500 حرف")]
        public string? Skills { get; set; }

        [StringLength(100, ErrorMessage = "الشهادات يجب ألا تزيد عن 100 حرف")]
        public string? Certifications { get; set; }

        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تزيد عن 1000 حرف")]
        public string? Notes { get; set; }

        [Range(0, 5, ErrorMessage = "التقييم يجب أن يكون بين 0 و 5")]
        public decimal? PerformanceRating { get; set; }

        public DateTime? TerminationDate { get; set; }

        [Url(ErrorMessage = "رابط الصورة الشخصية غير صحيح")]
        public string? ProfileImageUrl { get; set; }
    }

}
