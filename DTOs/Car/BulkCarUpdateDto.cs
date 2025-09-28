namespace CarDealershipAPI.DTOs.Car
{
    public class BulkCarUpdateDto
    {
        public List<int> CarIds { get; set; } = new List<int>();
        public string? Status { get; set; }
        public decimal? PriceAdjustment { get; set; } 
        public bool ApplyPriceAdjustmentAsPercentage { get; set; } = false;
    }
}
