namespace CarDealershipAPI.DTOs.common
{
    public class AppSettingsDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;
        public string CompanyEmail { get; set; } = string.Empty;
        public string CompanyLogo { get; set; } = string.Empty;
        public decimal DefaultTaxRate { get; set; } = 14;
        public decimal DefaultCommissionRate { get; set; } = 0.05m;
        public string DefaultCurrency { get; set; } = "EGP";
        public string TimeZone { get; set; } = "Africa/Cairo";
        public bool EnableNotifications { get; set; } = true;
        public bool EnableAuditLog { get; set; } = true;
        public int SessionTimeoutMinutes { get; set; } = 30;
        public int PageSizeDefault { get; set; } = 10;
        public int PageSizeMax { get; set; } = 100;
    }

}
