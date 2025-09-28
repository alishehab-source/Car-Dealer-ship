using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم العميل مطلوب")]
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

        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "رقم الهوية مطلوب")]
        [StringLength(20)]
        public string IdentityNumber { get; set; } = string.Empty;

        [StringLength(20)]
        [RegularExpression("^(NationalID|Passport)$")]
        public string IdentityType { get; set; } = "NationalID";

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        [RegularExpression("^(Male|Female)$")]
        public string? Gender { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

     
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }

}
