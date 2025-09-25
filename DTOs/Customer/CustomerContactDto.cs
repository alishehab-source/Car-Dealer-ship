using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerContactDto
    {
        [Required(ErrorMessage = "معرف العميل مطلوب")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "نوع التواصل مطلوب")]
        [RegularExpression("^(Phone|Email|SMS|WhatsApp|InPerson|Other)$", ErrorMessage = "نوع التواصل غير صحيح")]
        public string ContactType { get; set; } = string.Empty;

        [Required(ErrorMessage = "الوصف مطلوب")]
        [StringLength(1000, ErrorMessage = "الوصف يجب ألا يزيد عن 1000 حرف")]
        public string Description { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "النتيجة يجب ألا تزيد عن 500 حرف")]
        public string? Outcome { get; set; }

        public DateTime? NextContactDate { get; set; }

        [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
        public int? Rating { get; set; }
    }

}
