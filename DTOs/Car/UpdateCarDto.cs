using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTO.Car
{
    public class UpdateCarDto
    {
        [Required(ErrorMessage = "معرف السيارة مطلوب")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "الماركة يجب أن تكون بين 2 و 50 حرف")]
        public string? Make { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "الموديل يجب أن يكون بين 2 و 50 حرف")]
        public string? Model { get; set; }

        [Range(1980, 2030, ErrorMessage = "سنة الصنع يجب أن تكون بين 1980 و 2030")]
        public int? Year { get; set; }

        [Range(0.01, 9999999.99, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public decimal? Price { get; set; }

        [StringLength(50, ErrorMessage = "اللون يجب ألا يزيد عن 50 حرف")]
        public string? Color { get; set; }

        [Range(0, 999999, ErrorMessage = "عدد الكيلومترات يجب أن يكون رقم موجب")]
        public int? Mileage { get; set; }

        [RegularExpression("^(Gasoline|Diesel|Hybrid|Electric|CNG)$", ErrorMessage = "نوع الوقود يجب أن يكون Gasoline أو Diesel أو Hybrid أو Electric أو CNG")]
        public string? FuelType { get; set; }

        [RegularExpression("^(Manual|Automatic|CVT)$", ErrorMessage = "ناقل الحركة يجب أن يكون Manual أو Automatic أو CVT")]
        public string? Transmission { get; set; }

        [Range(1.0, 8.0, ErrorMessage = "سعة المحرك يجب أن تكون بين 1.0 و 8.0 لتر")]
        public double? EngineSize { get; set; }

        [Range(50, 1000, ErrorMessage = "قوة المحرك يجب أن تكون بين 50 و 1000 حصان")]
        public int? Horsepower { get; set; }

        [RegularExpression("^(New|Used|Certified)$", ErrorMessage = "حالة السيارة يجب أن تكون New أو Used أو Certified")]
        public string? Condition { get; set; }

        [RegularExpression("^(Available|Sold|Reserved|UnderMaintenance)$", ErrorMessage = "حالة التوفر يجب أن تكون Available أو Sold أو Reserved أو UnderMaintenance")]
        public string? Status { get; set; }

        [Range(2, 8, ErrorMessage = "عدد الأبواب يجب أن يكون بين 2 و 8")]
        public int? Doors { get; set; }

        [Range(2, 9, ErrorMessage = "عدد المقاعد يجب أن يكون بين 2 و 9")]
        public int? Seats { get; set; }

        [StringLength(1000, ErrorMessage = "الوصف يجب ألا يزيد عن 1000 حرف")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "المواصفات يجب ألا تزيد عن 500 حرف")]
        public string? Features { get; set; }

        [StringLength(500, ErrorMessage = "الملاحظات يجب ألا تزيد عن 500 حرف")]
        public string? Notes { get; set; }

        [Url(ErrorMessage = "رابط الصورة الرئيسية غير صحيح")]
        public string? MainImageUrl { get; set; }

        public List<string>? ImageUrls { get; set; }
    }

}
