using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeePerformanceUpdateDto
    {
        [Required(ErrorMessage = "معرف الموظف مطلوب")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "التقييم مطلوب")]
        [Range(0, 5, ErrorMessage = "التقييم يجب أن يكون بين 0 و 5")]
        public decimal Rating { get; set; }

        [Required(ErrorMessage = "الفترة مطلوبة")]
        [RegularExpression("^(Monthly|Quarterly|Annual)$", ErrorMessage = "الفترة يجب أن تكون Monthly أو Quarterly أو Annual")]
        public string Period { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "التعليقات يجب ألا تزيد عن 1000 حرف")]
        public string? Comments { get; set; }

        public List<PerformanceGoalDto>? Goals { get; set; }
    }

}
