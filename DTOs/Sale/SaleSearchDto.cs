namespace CarDealershipAPI.DTOs.Sale
{
    public class SaleSearchDto
    {
        public int? CarId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? SaleDateFrom { get; set; }
        public DateTime? SaleDateTo { get; set; }
        public decimal? MinSalePrice { get; set; }
        public decimal? MaxSalePrice { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public string? InvoiceNumber { get; set; }

        public string? CustomerName { get; set; }
        public string? EmployeeName { get; set; }
        public string? CarMake { get; set; }
        public string? CarModel { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; } = "SaleDate";
        public bool SortDescending { get; set; } = true;
    }

}
