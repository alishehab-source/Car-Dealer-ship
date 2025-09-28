using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999999.99)]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99)]
        public decimal Discount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99)]
        public decimal Tax { get; set; } = 0;

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal TaxRate { get; set; } = 14;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999999.99)]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^(Cash|Check|CreditCard|BankTransfer|Mixed)$")]
        public string PaymentMethod { get; set; } = "Cash";

        [Required]
        [StringLength(20)]
        [RegularExpression("^(Pending|Completed|Cancelled)$")]
        public string Status { get; set; } = "Pending";

        [StringLength(50)]
        public string? InvoiceNumber { get; set; }

        public DateTime? CompletionDate { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? EmployeeCommission { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        public Car Car { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;

        public void CalculateTotalAmount()
        {
            TotalAmount = SalePrice - Discount + Tax;
        }

        public void CalculateEmployeeCommission()
        {
            if (Employee != null)
            {
                EmployeeCommission = SalePrice * Employee.CommissionRate;
            }
        }

        public void CalculateTax()
        {
            Tax = SalePrice * (TaxRate / 100);
        }
    }

}

