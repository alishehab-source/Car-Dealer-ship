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
        [StringLength(100, ErrorMessage = "البريد الإلكتروني لا يجب أن يتجاوز 100 حرف")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "رقم الهاتف يجب أن يكون بين 7 و 20 رقم")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهوية مطلوب")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "رقم الهوية يجب أن يكون بين 10 و 20 رقم")]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ التوظيف مطلوب")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "المنصب مطلوب")]
        [RegularExpression("^(Manager|SalesRep|Admin)$", ErrorMessage = "المنصب يجب أن يكون Manager أو SalesRep أو Admin")]
        public string Role { get; set; } = "SalesRep";

        [Required(ErrorMessage = "القسم مطلوب")]
        [RegularExpression("^(Sales|Administration|Management)$", ErrorMessage = "القسم يجب أن يكون Sales أو Administration أو Management")]
        public string Department { get; set; } = "Sales";

        [Required(ErrorMessage = "الراتب الأساسي مطلوب")]
        [Range(0.01, 999999.99, ErrorMessage = "الراتب الأساسي يجب أن يكون أكبر من صفر")]
        public decimal BaseSalary { get; set; }

        [Range(0, 1, ErrorMessage = "معدل العمولة يجب أن يكون بين 0 و 1")]
        public decimal CommissionRate { get; set; } = 0.05m;

        [Required(ErrorMessage = "حالة الموظف مطلوبة")]
        [RegularExpression("^(Active|Inactive|OnLeave)$", ErrorMessage = "حالة الموظف يجب أن تكون Active أو Inactive أو OnLeave")]
        public string Status { get; set; } = "Active";

        public int? SupervisorId { get; set; }
    }

}
