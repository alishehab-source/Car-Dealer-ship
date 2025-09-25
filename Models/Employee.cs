using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الموظف مطلوب")]
        [StringLength(100, ErrorMessage = "اسم الموظف يجب ألا يزيد عن 100 حرف")]
        [MinLength(2, ErrorMessage = "اسم الموظف يجب ألا يقل عن حرفين")]
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

        [StringLength(10, ErrorMessage = "الجنس يجب ألا يزيد عن 10 أحرف")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "الجنس يجب أن يكون Male أو Female أو Other")]
        public string? Gender { get; set; }

        [StringLength(20, ErrorMessage = "الحالة الاجتماعية يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Single|Married|Divorced|Widowed)$", ErrorMessage = "الحالة الاجتماعية يجب أن تكون Single أو Married أو Divorced أو Widowed")]
        public string? MaritalStatus { get; set; }

        [Required(ErrorMessage = "رقم الهوية مطلوب")]
        [StringLength(20, ErrorMessage = "رقم الهوية يجب ألا يزيد عن 20 حرف")]
        public string IdentityNumber { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "نوع الهوية يجب ألا يزيد عن 20 حرف")]
        [RegularExpression("^(NationalID|Passport|ResidencePermit)$", ErrorMessage = "نوع الهوية يجب أن يكون NationalID أو Passport أو ResidencePermit")]
        public string IdentityType { get; set; } = "NationalID";

        [Required(ErrorMessage = "تاريخ التوظيف مطلوب")]
        public DateTime HireDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        [Required(ErrorMessage = "المسمى الوظيفي مطلوب")]
        [StringLength(50, ErrorMessage = "المسمى الوظيفي يجب ألا يزيد عن 50 حرف")]
        [RegularExpression("^(Manager|SalesRep|Accountant|Mechanic|Receptionist|Admin|Other)$", ErrorMessage = "المسمى الوظيفي يجب أن يكون Manager أو SalesRep أو Accountant أو Mechanic أو Receptionist أو Admin أو Other")]
        public string Role { get; set; } = "SalesRep";

        [StringLength(50, ErrorMessage = "القسم يجب ألا يزيد عن 50 حرف")]
        [RegularExpression("^(Sales|Finance|Service|Administration|Management|IT|HR)$", ErrorMessage = "القسم يجب أن يكون Sales أو Finance أو Service أو Administration أو Management أو IT أو HR")]
        public string Department { get; set; } = "Sales";

        [Required(ErrorMessage = "الراتب الأساسي مطلوب")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "الراتب الأساسي يجب أن يكون أكبر من صفر")]
        public decimal BaseSalary { get; set; }

        [Column(TypeName = "decimal(5,4)")]
        [Range(0, 1, ErrorMessage = "نسبة العمولة يجب أن تكون بين 0 و 1")]
        public decimal CommissionRate { get; set; } = 0.05m; // 5% default

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999.99, ErrorMessage = "الحد الأقصى للعمولة يجب أن يكون رقم موجب")]
        public decimal? MaxCommission { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999.99, ErrorMessage = "البونص يجب أن يكون رقم موجب")]
        public decimal? Bonus { get; set; }

        [Range(0, 99, ErrorMessage = "أيام الإجازة يجب أن تكون بين 0 و 99")]
        public int AnnualLeave { get; set; } = 21;

        [Range(0, 99, ErrorMessage = "أيام الإجازة المستخدمة يجب أن تكون بين 0 و 99")]
        public int UsedLeave { get; set; } = 0;

        [Range(0, 99, ErrorMessage = "أيام الإجازة المرضية يجب أن تكون بين 0 و 99")]
        public int SickLeave { get; set; } = 7;

        [Range(0, 99, ErrorMessage = "أيام الإجازة المرضية المستخدمة يجب أن تكون بين 0 و 99")]
        public int UsedSickLeave { get; set; } = 0;

        [Required(ErrorMessage = "حالة الموظف مطلوبة")]
        [StringLength(20, ErrorMessage = "حالة الموظف يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Active|Inactive|OnLeave|Terminated|Suspended)$", ErrorMessage = "حالة الموظف يجب أن تكون Active أو Inactive أو OnLeave أو Terminated أو Suspended")]
        public string Status { get; set; } = "Active";

        [StringLength(20, ErrorMessage = "نوع العمل يجب ألا يزيد عن 20 حرف")]
        [RegularExpression("^(FullTime|PartTime|Contract|Intern|Consultant)$", ErrorMessage = "نوع العمل يجب أن يكون FullTime أو PartTime أو Contract أو Intern أو Consultant")]
        public string EmploymentType { get; set; } = "FullTime";

        public int? SupervisorId { get; set; }

        [StringLength(100, ErrorMessage = "المؤهل العلمي يجب ألا يزيد عن 100 حرف")]
        public string? Education { get; set; }

        [StringLength(500, ErrorMessage = "المهارات يجب ألا تزيد عن 500 حرف")]
        public string? Skills { get; set; }

        [StringLength(100, ErrorMessage = "الشهادات يجب ألا تزيد عن 100 حرف")]
        public string? Certifications { get; set; }

        [StringLength(20, ErrorMessage = "فصيلة الدم يجب ألا تزيد عن 20 حرف")]
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

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        [Range(0, 999, ErrorMessage = "عدد المبيعات يجب أن يكون رقم موجب")]
        public int TotalSales { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99, ErrorMessage = "إجمالي قيمة المبيعات يجب أن تكون رقم موجب")]
        public decimal TotalSalesValue { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99, ErrorMessage = "إجمالي العمولات يجب أن تكون رقم موجب")]
        public decimal TotalCommissionEarned { get; set; } = 0;

        public DateTime? LastSaleDate { get; set; }

        [Range(0, 5, ErrorMessage = "التقييم يجب أن يكون بين 0 و 5")]
        public decimal? PerformanceRating { get; set; }

        // Navigation Properties
        public Employee? Supervisor { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<TestDrive> TestDrives { get; set; } = new List<TestDrive>();

        // Employee Evaluations, Training Records, etc. (إضافة مستقبلية)
        // public ICollection<EmployeeEvaluation> Evaluations { get; set; } = new List<EmployeeEvaluation>();
        // public ICollection<TrainingRecord> TrainingRecords { get; set; } = new List<TrainingRecord>();
        // public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}

