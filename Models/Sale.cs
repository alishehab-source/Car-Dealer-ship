using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "معرف السيارة مطلوب")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "معرف العميل مطلوب")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "معرف الموظف مطلوب")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "تاريخ البيع مطلوب")]
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "سعر البيع مطلوب")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999999.99, ErrorMessage = "سعر البيع يجب أن يكون أكبر من صفر")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "سعر السيارة الأصلي يجب أن يكون رقم موجب")]
        public decimal? OriginalCarPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "مقدار الخصم يجب أن يكون رقم موجب")]
        public decimal Discount { get; set; } = 0;

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "نسبة الخصم يجب أن تكون بين 0 و 100")]
        public decimal? DiscountPercentage { get; set; }

        [StringLength(100, ErrorMessage = "سبب الخصم يجب ألا يزيد عن 100 حرف")]
        public string? DiscountReason { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "الضرائب يجب أن تكون رقم موجب")]
        public decimal Tax { get; set; } = 0;

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "نسبة الضريبة يجب أن تكون بين 0 و 100")]
        public decimal TaxRate { get; set; } = 14; // Default VAT in Egypt

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "الرسوم الإضافية يجب أن تكون رقم موجب")]
        public decimal AdditionalFees { get; set; } = 0;

        [StringLength(200, ErrorMessage = "تفاصيل الرسوم الإضافية يجب ألا تزيد عن 200 حرف")]
        public string? AdditionalFeesDetails { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999999.99, ErrorMessage = "المبلغ الإجمالي يجب أن يكون أكبر من صفر")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "طريقة الدفع مطلوبة")]
        [StringLength(20, ErrorMessage = "طريقة الدفع يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Cash|Check|CreditCard|BankTransfer|Financing|Mixed)$", ErrorMessage = "طريقة الدفع يجب أن تكون Cash أو Check أو CreditCard أو BankTransfer أو Financing أو Mixed")]
        public string PaymentMethod { get; set; } = "Cash";

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "المقدم يجب أن يكون رقم موجب")]
        public decimal DownPayment { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "المبلغ المتبقي يجب أن يكون رقم موجب")]
        public decimal RemainingAmount { get; set; } = 0;

        public bool IsFinanced { get; set; } = false;

        [StringLength(100, ErrorMessage = "جهة التمويل يجب ألا تزيد عن 100 حرف")]
        public string? FinancingCompany { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 50, ErrorMessage = "معدل الفائدة يجب أن يكون بين 0 و 50")]
        public decimal? InterestRate { get; set; }

        [Range(1, 120, ErrorMessage = "مدة التمويل بالشهور يجب أن تكون بين 1 و 120")]
        public int? FinancingTermMonths { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999.99, ErrorMessage = "القسط الشهري يجب أن يكون رقم موجب")]
        public decimal? MonthlyPayment { get; set; }

        [StringLength(50, ErrorMessage = "رقم الشيك يجب ألا يزيد عن 50 حرف")]
        public string? CheckNumber { get; set; }

        [StringLength(100, ErrorMessage = "البنك يجب ألا يزيد عن 100 حرف")]
        public string? BankName { get; set; }

        [StringLength(50, ErrorMessage = "رقم المرجع يجب ألا يزيد عن 50 حرف")]
        public string? TransactionReference { get; set; }

        [StringLength(50, ErrorMessage = "رقم الفاتورة يجب ألا يزيد عن 50 حرف")]
        public string? InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [Required(ErrorMessage = "حالة البيع مطلوبة")]
        [StringLength(20, ErrorMessage = "حالة البيع يجب ألا تزيد عن 20 حرف")]
        [RegularExpression("^(Pending|Completed|Cancelled|Refunded|OnHold)$", ErrorMessage = "حالة البيع يجب أن تكون Pending أو Completed أو Cancelled أو Refunded أو OnHold")]
        public string Status { get; set; } = "Pending";

        [StringLength(200, ErrorMessage = "سبب الإلغاء يجب ألا يزيد عن 200 حرف")]
        public string? CancellationReason { get; set; }

        public DateTime? CancellationDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [StringLength(200, ErrorMessage = "مكان التسليم يجب ألا يزيد عن 200 حرف")]
        public string? DeliveryLocation { get; set; }

        [StringLength(100, ErrorMessage = "اسم من استلم السيارة يجب ألا يزيد عن 100 حرف")]
        public string? DeliveredBy { get; set; }

        [StringLength(100, ErrorMessage = "اسم من سلم السيارة يجب ألا يزيد عن 100 حرف")]
        public string? ReceivedBy { get; set; }

        public bool HasWarranty { get; set; } = true;

        [Range(0, 120, ErrorMessage = "مدة الضمان بالشهور يجب أن تكون بين 0 و 120")]
        public int? WarrantyMonths { get; set; } = 12;

        public DateTime? WarrantyStartDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }

        [Range(0, 999999, ErrorMessage = "كيلومترات الضمان يجب أن تكون رقم موجب")]
        public int? WarrantyMileage { get; set; }

        [StringLength(200, ErrorMessage = "تفاصيل الضمان يجب ألا تزيد عن 200 حرف")]
        public string? WarrantyDetails { get; set; }

        public bool HasInsurance { get; set; } = false;

        [StringLength(100, ErrorMessage = "شركة التأمين يجب ألا تزيد عن 100 حرف")]
        public string? InsuranceCompany { get; set; }

        [StringLength(50, ErrorMessage = "رقم بوليصة التأمين يجب ألا يزيد عن 50 حرف")]
        public string? InsurancePolicyNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999.99, ErrorMessage = "قيمة التأمين يجب أن تكون رقم موجب")]
        public decimal? InsuranceAmount { get; set; }

        public DateTime? InsuranceStartDate { get; set; }

        public DateTime? InsuranceEndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99, ErrorMessage = "عمولة الموظف يجب أن تكون رقم موجب")]
        public decimal? EmployeeCommission { get; set; }

        [Column(TypeName = "decimal(5,4)")]
        [Range(0, 1, ErrorMessage = "نسبة عمولة الموظف يجب أن تكون بين 0 و 1")]
        public decimal? EmployeeCommissionRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(-9999999.99, 9999999.99, ErrorMessage = "هامش الربح يجب أن يكون رقم صحيح")]
        public decimal? ProfitMargin { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(-100, 1000, ErrorMessage = "نسبة هامش الربح يجب أن تكون رقم صحيح")]
        public decimal? ProfitMarginPercentage { get; set; }

        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تزيد عن 1000 حرف")]
        public string? Notes { get; set; }

        [StringLength(500, ErrorMessage = "الشروط الخاصة يجب ألا تزيد عن 500 حرف")]
        public string? SpecialTerms { get; set; }

        [Range(1, 5, ErrorMessage = "تقييم العميل يجب أن يكون بين 1 و 5")]
        public int? CustomerRating { get; set; }

        [StringLength(500, ErrorMessage = "تعليق العميل يجب ألا يزيد عن 500 حرف")]
        public string? CustomerFeedback { get; set; }

        public bool IsTestDriveCompleted { get; set; } = false;

        public int? TestDriveId { get; set; }

        [StringLength(100, ErrorMessage = "مصدر العميل يجب ألا يزيد عن 100 حرف")]
        public string? CustomerSource { get; set; }

        public bool IsTradeIn { get; set; } = false;

        [StringLength(50, ErrorMessage = "ماركة السيارة المستبدلة يجب ألا تزيد عن 50 حرف")]
        public string? TradeInMake { get; set; }

        [StringLength(50, ErrorMessage = "موديل السيارة المستبدلة يجب ألا تزيد عن 50 حرف")]
        public string? TradeInModel { get; set; }

        [Range(1980, 2030, ErrorMessage = "سنة السيارة المستبدلة يجب أن تكون بين 1980 و 2030")]
        public int? TradeInYear { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "قيمة السيارة المستبدلة يجب أن تكون رقم موجب")]
        public decimal? TradeInValue { get; set; }

        [StringLength(17, MinimumLength = 17, ErrorMessage = "رقم شاسيه السيارة المستبدلة يجب أن يكون 17 حرف بالضبط")]
        public string? TradeInVIN { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        [StringLength(100, ErrorMessage = "المستخدم المنشئ يجب ألا يزيد عن 100 حرف")]
        public string? CreatedBy { get; set; }

        [StringLength(100, ErrorMessage = "المستخدم المحدث يجب ألا يزيد عن 100 حرف")]
        public string? UpdatedBy { get; set; }

        // Navigation Properties
        public Car Car { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public TestDrive? TestDrive { get; set; }

        // Additional related entities (إضافة مستقبلية)
        // public ICollection<SaleDocument> Documents { get; set; } = new List<SaleDocument>();
        // public ICollection<PaymentSchedule> PaymentSchedules { get; set; } = new List<PaymentSchedule>();
        // public ICollection<ServiceReminder> ServiceReminders { get; set; } = new List<ServiceReminder>();
    }
}

