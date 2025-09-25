using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTO.Car
{
    public class CreateCarDto
    {
        [Required(ErrorMessage = "الماركة مطلوبة")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "الماركة يجب أن تكون بين 2 و 50 حرف")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "الموديل مطلوب")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "الموديل يجب أن يكون بين 2 و 50 حرف")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "سنة الصنع مطلوبة")]
        [Range(1980, 2030, ErrorMessage = "سنة الصنع يجب أن تكون بين 1980 و 2030")]
        public int Year { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0.01, 9999999.99, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public decimal Price { get; set; }

        [StringLength(17, MinimumLength = 17, ErrorMessage = "رقم الشاسيه يجب أن يكون 17 حرف بالضبط")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "رقم الشاسيه غير صحيح")]
        public string? VIN { get; set; }

        [StringLength(50, ErrorMessage = "اللون يجب ألا يزيد عن 50 حرف")]
        public string? Color { get; set; } = "أبيض";

        [Range(0, 999999, ErrorMessage = "عدد الكيلومترات يجب أن يكون رقم موجب")]
        public int Mileage { get; set; } = 0;

        [Required(ErrorMessage = "نوع الوقود مطلوب")]
        [RegularExpression("^(Gasoline|Diesel|Hybrid|Electric|CNG)$", ErrorMessage = "نوع الوقود يجب أن يكون Gasoline أو Diesel أو Hybrid أو Electric أو CNG")]
        public string FuelType { get; set; } = "Gasoline";

        [Required(ErrorMessage = "ناقل الحركة مطلوب")]
        [RegularExpression("^(Manual|Automatic|CVT)$", ErrorMessage = "ناقل الحركة يجب أن يكون Manual أو Automatic أو CVT")]
        public string Transmission { get; set; } = "Manual";

        [Range(1.0, 8.0, ErrorMessage = "سعة المحرك يجب أن تكون بين 1.0 و 8.0 لتر")]
        public double? EngineSize { get; set; }

        [Range(50, 1000, ErrorMessage = "قوة المحرك يجب أن تكون بين 50 و 1000 حصان")]
        public int? Horsepower { get; set; }

        [Required(ErrorMessage = "حالة السيارة مطلوبة")]
        [RegularExpression("^(New|Used|Certified)$", ErrorMessage = "حالة السيارة يجب أن تكون New أو Used أو Certified")]
        public string Condition { get; set; } = "Used";

        [Range(2, 8, ErrorMessage = "عدد الأبواب يجب أن يكون بين 2 و 8")]
        public int? Doors { get; set; } = 4;

        [Range(2, 9, ErrorMessage = "عدد المقاعد يجب أن يكون بين 2 و 9")]
        public int? Seats { get; set; } = 5;

        [StringLength(50, ErrorMessage = "نوع الدفع يجب ألا يزيد عن 50 حرف")]
        public string? DriveType { get; set; } = "FWD";

        [StringLength(1000, ErrorMessage = "الوصف يجب ألا يزيد عن 1000 حرف")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "المواصفات يجب ألا تزيد عن 500 حرف")]
        public string? Features { get; set; }

        [StringLength(500, ErrorMessage = "الملاحظات يجب ألا تزيد عن 500 حرف")]
        public string? Notes { get; set; }

        [Url(ErrorMessage = "رابط الصورة الرئيسية غير صحيح")]
        public string? MainImageUrl { get; set; }

        public List<string>? ImageUrls { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "سعر الشراء يجب أن يكون رقم موجب")]
        public decimal? PurchasePrice { get; set; }

        public DateTime? PurchaseDate { get; set; }

        [StringLength(100, ErrorMessage = "مصدر الشراء يجب ألا يزيد عن 100 حرف")]
        public string? Source { get; set; }
    }

}
