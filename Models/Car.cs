using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "الماركة مطلوبة")]
        [StringLength(50, ErrorMessage = "اسم الماركة يجب ألا يزيد عن 50 حرف")]
        [MinLength(2, ErrorMessage = "اسم الماركة يجب ألا يقل عن حرفين")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "الموديل مطلوب")]
        [StringLength(50, ErrorMessage = "اسم الموديل يجب ألا يزيد عن 50 حرف")]
        [MinLength(2, ErrorMessage = "اسم الموديل يجب ألا يقل عن حرفين")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "سنة الصنع مطلوبة")]
        [Range(1980, 2030, ErrorMessage = "سنة الصنع يجب أن تكون بين 1980 و 2030")]
        public int Year { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999999.99, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public decimal Price { get; set; }

        [StringLength(17, MinimumLength = 17, ErrorMessage = "رقم الشاسيه يجب أن يكون 17 حرف بالضبط")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "رقم الشاسيه غير صحيح")]
        public string? VIN { get; set; }

        [StringLength(50, ErrorMessage = "اللون يجب ألا يزيد عن 50 حرف")]
        public string? Color { get; set; } = "أبيض";

        [Range(0, 999999, ErrorMessage = "عدد الكيلومترات يجب أن يكون رقم موجب")]
        public int Mileage { get; set; } = 0;

        [StringLength(20, ErrorMessage = "نوع الوقود يجب ألا يزيد عن 20 حرف")]
        [RegularExpression("^(Gasoline|Diesel|Hybrid|Electric|CNG)$", ErrorMessage = "نوع الوقود يجب أن يكون Gasoline أو Diesel أو Hybrid أو Electric أو CNG")]
        public string FuelType { get; set; } = "Gasoline";

        [StringLength(20, ErrorMessage = "ناقل الحركة يجب ألا يزيد عن 20 حرف")]
        [RegularExpression("^(Manual|Automatic|CVT)$", ErrorMessage = "ناقل الحركة يجب أن يكون Manual أو Automatic أو CVT")]
        public string Transmission { get; set; } = "Manual";

        [Range(1.0, 8.0, ErrorMessage = "سعة المحرك يجب أن تكون بين 1.0 و 8.0 لتر")]
        public double? EngineSize { get; set; }

        [Range(50, 1000, ErrorMessage = "قوة المحرك يجب أن تكون بين 50 و 1000 حصان")]
        public int? Horsepower { get; set; }

        [StringLength(20, ErrorMessage = "حالة السيارة يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(New|Used|Certified)$", ErrorMessage = "حالة السيارة يجب أن تكون New أو Used أو Certified")]
        public string Condition { get; set; } = "Used";

        [Required(ErrorMessage = "حالة التوفر مطلوبة")]
        [StringLength(20, ErrorMessage = "حالة التوفر يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Available|Sold|Reserved|UnderMaintenance)$", ErrorMessage = "حالة التوفر يجب أن تكون Available أو Sold أو Reserved أو UnderMaintenance")]
        public string Status { get; set; } = "Available";

        [Range(2, 8, ErrorMessage = "عدد الأبواب يجب أن يكون بين 2 و 8")]
        public int? Doors { get; set; } = 4;

        [Range(2, 9, ErrorMessage = "عدد المقاعد يجب أن يكون بين 2 و 9")]
        public int? Seats { get; set; } = 5;

        [StringLength(50, ErrorMessage = "الدفع الرباعي يجب ألا يزيد عن 50 حرف")]
        public string? DriveType { get; set; } = "FWD"; // FWD, RWD, AWD, 4WD

        [StringLength(1000, ErrorMessage = "الوصف يجب ألا يزيد عن 1000 حرف")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "المواصفات يجب ألا تزيد عن 500 حرف")]
        public string? Features { get; set; }

        [StringLength(500, ErrorMessage = "الملاحظات يجب ألا تزيد عن 500 حرف")]
        public string? Notes { get; set; }

        [Url(ErrorMessage = "رابط الصورة الرئيسية غير صحيح")]
        public string? MainImageUrl { get; set; }

        [StringLength(2000, ErrorMessage = "روابط الصور يجب ألا تزيد عن 2000 حرف")]
        public string? ImageUrls { get; set; } // JSON array of image URLs

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "سعر الشراء يجب أن يكون رقم موجب")]
        public decimal? PurchasePrice { get; set; }

        public DateTime? PurchaseDate { get; set; }

        [StringLength(100, ErrorMessage = "مصدر الشراء يجب ألا يزيد عن 100 حرف")]
        public string? Source { get; set; } // Auction, Trade-in, Private, etc.

        public bool IsFinanced { get; set; } = false;

        [StringLength(100, ErrorMessage = "شركة التمويل يجب ألا تزيد عن 100 حرف")]
        public string? FinancingCompany { get; set; }

        public DateTime? LastServiceDate { get; set; }

        public DateTime? NextServiceDate { get; set; }

        [Range(0, 99, ErrorMessage = "عدد المالكين السابقين يجب أن يكون بين 0 و 99")]
        public int? PreviousOwners { get; set; }

        public bool HasAccidents { get; set; } = false;

        [StringLength(1000, ErrorMessage = "تاريخ الحوادث يجب ألا يزيد عن 1000 حرف")]
        public string? AccidentHistory { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public DateTime? SoldDate { get; set; }

        // Navigation Properties - للـ relationships مع الـ entities التانية
        // هيتم ربطها في الـ DbContext
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<TestDrive> TestDrives { get; set; } = new List<TestDrive>();
    }
}

