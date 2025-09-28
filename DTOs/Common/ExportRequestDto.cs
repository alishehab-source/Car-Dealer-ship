using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.common
{
    public class ExportRequestDto
    {
        [Required(ErrorMessage = "نوع التصدير مطلوب")]
        [RegularExpression("^(Excel|PDF|CSV)$", ErrorMessage = "نوع التصدير يجب أن يكون Excel أو PDF أو CSV")]
        public string ExportType { get; set; } = "Excel";

        [Required(ErrorMessage = "نوع البيانات مطلوب")]
        [RegularExpression("^(Cars|Customers|Employees|Sales)$", ErrorMessage = "نوع البيانات غير صحيح")]
        public string DataType { get; set; } = "Cars";

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<string> SelectedFields { get; set; } = new List<string>();
        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();
        public bool IncludeDeleted { get; set; } = false;
    }

}
