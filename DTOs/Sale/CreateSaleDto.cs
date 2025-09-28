using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.DTOs.Sale
{
    public class CreateSaleDto
    {
        [Required(ErrorMessage = "معرف السيارة مطلوب")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "معرف العميل مطلوب")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "معرف الموظف مطلوب")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "تاريخ البيع مطلوب")]
        [DataType(DataType.DateTime)]
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "سعر البيع مطلوب")]
        [Range(0.01, 9999999.99, ErrorMessage = "سعر البيع يجب أن يكون أكبر من صفر")]
        public decimal SalePrice { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "الخصم يجب أن يكون صفر أو أكبر")]
        public decimal Discount { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "معدل الضريبة يجب أن يكون بين 0 و 100")]
        public decimal TaxRate { get; set; } = 14;

        [Required(ErrorMessage = "طريقة الدفع مطلوبة")]
        [RegularExpression("^(Cash|Check|CreditCard|BankTransfer|Mixed)$", ErrorMessage = "طريقة الدفع غير صحيحة")]
        public string PaymentMethod { get; set; } = "Cash";

        [Required(ErrorMessage = "حالة البيع مطلوبة")]
        [RegularExpression("^(Pending|Completed|Cancelled)$", ErrorMessage = "حالة البيع غير صحيحة")]
        public string Status { get; set; } = "Pending";

        [StringLength(50, ErrorMessage = "رقم الفاتورة لا يجب أن يتجاوز 50 حرف")]
        public string? InvoiceNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CompletionDate { get; set; }

        [StringLength(500, ErrorMessage = "الملاحظات لا يجب أن تتجاوز 500 حرف")]
        public string? Notes { get; set; }
    }

}
