using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الموظف مطلوب")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(20)]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهوية مطلوب")]
        [StringLength(20)]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ التوظيف مطلوب")]
        public DateTime HireDate { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^(Manager|SalesRep|Admin)$")]
        public string Role { get; set; } = "SalesRep";

        [StringLength(50)]
        [RegularExpression("^(Sales|Administration|Management)$")]
        public string Department { get; set; } = "Sales";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99)]
        public decimal BaseSalary { get; set; }

        [Column(TypeName = "decimal(5,4)")]
        [Range(0, 1)]
        public decimal CommissionRate { get; set; } = 0.05m;

        [Required]
        [StringLength(20)]
        [RegularExpression("^(Active|Inactive|OnLeave)$")]
        public string Status { get; set; } = "Active";

        public int? SupervisorId { get; set; }

        [Range(0, 999)]
        public int TotalSales { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99)]
        public decimal TotalSalesValue { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99)]
        public decimal TotalCommissionEarned { get; set; } = 0;

        public DateTime? LastSaleDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        public Employee? Supervisor { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }

}

