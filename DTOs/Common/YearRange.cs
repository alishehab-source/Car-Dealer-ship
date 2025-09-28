namespace CarDealershipAPI.DTOs.common
{
    public class YearRange
    {
        public int Min { get; set; } = 1980;
        public int Max { get; set; } = DateTime.Now.Year + 1;
    }

}
