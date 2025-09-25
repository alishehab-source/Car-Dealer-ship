using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Employee
{
    public class PerformanceGoalDto
    {
        [Required(ErrorMessage = "اسم الهدف مطلوب")]
        [StringLength(200, ErrorMessage = "اسم الهدف يجب ألا يزيد عن 200 حرف")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "الوصف يجب ألا يزيد عن 500 حرف")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "القيمة المستهدفة مطلوبة")]
        public decimal TargetValue { get; set; }

        public decimal? ActualValue { get; set; }

        [Required(ErrorMessage = "تاريخ الانتهاء مطلوب")]
        public DateTime DueDate { get; set; }

        [RegularExpression("^(Pending|InProgress|Completed|Overdue)$", ErrorMessage = "حالة الهدف غير صحيحة")]
        public string Status { get; set; } = "Pending";
    }

}
