using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "الماركة مطلوبة")]
        [StringLength(50)]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "الموديل مطلوب")]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "سنة الصنع مطلوبة")]
        [Range(1980, 2030)]
        public int Year { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999999.99)]
        public decimal Price { get; set; }

        [StringLength(17, MinimumLength = 17)]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$")]
        public string? VIN { get; set; }

        [StringLength(50)]
        public string? Color { get; set; } = "أبيض";

        [Range(0, 999999)]
        public int Mileage { get; set; } = 0;

        [StringLength(20)]
        [RegularExpression("^(Gasoline|Diesel|Hybrid|Electric)$")]
        public string FuelType { get; set; } = "Gasoline";

        [StringLength(20)]
        [RegularExpression("^(Manual|Automatic|CVT)$")]
        public string Transmission { get; set; } = "Manual";

        [StringLength(20)]
        [RegularExpression("^(New|Used|Certified)$")]
        public string Condition { get; set; } = "Used";

        [Required]
        [StringLength(20)]
        [RegularExpression("^(Available|Sold|Reserved)$")]
        public string Status { get; set; } = "Available";

        [Range(2, 8)]
        public int? Doors { get; set; } = 4;

        [Range(2, 9)]
        public int? Seats { get; set; } = 5;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }


        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
