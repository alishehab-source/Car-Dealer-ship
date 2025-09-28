using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Car
{
    public class CreateCarDto
    {
        [Required(ErrorMessage = "الماركة مطلوبة")]
        [StringLength(50, ErrorMessage = "الماركة لا يجب أن تتجاوز 50 حرف")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "الموديل مطلوب")]
        [StringLength(50, ErrorMessage = "الموديل لا يجب أن يتجاوز 50 حرف")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "سنة الصنع مطلوبة")]
        [Range(1980, 2030, ErrorMessage = "سنة الصنع يجب أن تكون بين 1980 و 2030")]
        public int Year { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0.01, 9999999.99, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public decimal Price { get; set; }

        [StringLength(17, MinimumLength = 17, ErrorMessage = "رقم الهيكل يجب أن يكون 17 رقم بالضبط")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "رقم الهيكل غير صحيح")]
        public string? VIN { get; set; }

        [StringLength(50, ErrorMessage = "اللون لا يجب أن يتجاوز 50 حرف")]
        public string? Color { get; set; } = "أبيض";

        [Range(0, 999999, ErrorMessage = "المسافة المقطوعة يجب أن تكون بين 0 و 999999")]
        public int Mileage { get; set; } = 0;

        [Required(ErrorMessage = "نوع الوقود مطلوب")]
        [RegularExpression("^(Gasoline|Diesel|Hybrid|Electric)$", ErrorMessage = "نوع الوقود غير صحيح")]
        public string FuelType { get; set; } = "Gasoline";

        [Required(ErrorMessage = "نوع ناقل الحركة مطلوب")]
        [RegularExpression("^(Manual|Automatic|CVT)$", ErrorMessage = "نوع ناقل الحركة غير صحيح")]
        public string Transmission { get; set; } = "Manual";

        [Required(ErrorMessage = "حالة السيارة مطلوبة")]
        [RegularExpression("^(New|Used|Certified)$", ErrorMessage = "حالة السيارة غير صحيحة")]
        public string Condition { get; set; } = "Used";

        [Required(ErrorMessage = "حالة التوفر مطلوبة")]
        [RegularExpression("^(Available|Sold|Reserved)$", ErrorMessage = "حالة التوفر غير صحيحة")]
        public string Status { get; set; } = "Available";

        [Range(2, 8, ErrorMessage = "عدد الأبواب يجب أن يكون بين 2 و 8")]
        public int? Doors { get; set; } = 4;

        [Range(2, 9, ErrorMessage = "عدد المقاعد يجب أن يكون بين 2 و 9")]
        public int? Seats { get; set; } = 5;

        [StringLength(500, ErrorMessage = "الوصف لا يجب أن يتجاوز 500 حرف")]
        public string? Description { get; set; }
    }

}
