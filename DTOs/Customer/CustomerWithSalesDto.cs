namespace CarDealershipAPI.DTOs.Customer
{
    public class CustomerWithSalesDto : CustomerDto
    {
        public List<CustomerSaleDto> RecentSales { get; set; } = new List<CustomerSaleDto>();
    }

}
