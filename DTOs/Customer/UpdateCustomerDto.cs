using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "اسم العميل يجب أن يكون بين 2 و 100 حرف")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [StringLength(100, ErrorMessage = "البريد الإلكتروني لا يجب أن يتجاوز 100 حرف")]
        public string? Email { get; set; }

        [StringLength(20, MinimumLength = 7, ErrorMessage = "رقم الهاتف يجب أن يكون بين 7 و 20 رقم")]
        [RegularExpression(@"^[\+]?[0-9\-\s\(\)]{7,20}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string? Phone { get; set; }

        [StringLength(200, ErrorMessage = "العنوان لا يجب أن يتجاوز 200 حرف")]
        public string? Address { get; set; }

        [StringLength(20, MinimumLength = 10, ErrorMessage = "رقم الهوية يجب أن يكون بين 10 و 20 رقم")]
        public string? IdentityNumber { get; set; }

        [RegularExpression("^(NationalID|Passport)$", ErrorMessage = "نوع الهوية يجب أن يكون NationalID أو Passport")]
        public string? IdentityType { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [RegularExpression("^(Male|Female)$", ErrorMessage = "الجنس يجب أن يكون Male أو Female")]
        public string? Gender { get; set; }

        [StringLength(500, ErrorMessage = "الملاحظات لا يجب أن تتجاوز 500 حرف")]
        public string? Notes { get; set; }
    }

}
