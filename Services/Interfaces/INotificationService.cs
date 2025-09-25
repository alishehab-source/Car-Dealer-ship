namespace CarDealershipAPI.Services.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendWelcomeEmailAsync(int customerId);
        Task<bool> SendTestDriveConfirmationAsync(int testDriveId);
        Task<bool> SendTestDriveReminderAsync(int testDriveId);
        Task<bool> SendSaleConfirmationAsync(int saleId);
        Task<bool> SendInvoiceAsync(int saleId);
        Task<bool> SendFollowUpReminderAsync(int customerId);

   
        Task<bool> SendTestDriveSMSReminderAsync(int testDriveId);
        Task<bool> SendSaleSMSConfirmationAsync(int saleId);
        Task<bool> SendPromotionalSMSAsync(int customerId, string message);


        Task<bool> SendTestDriveStartNotificationAsync(int testDriveId);
        Task<bool> SendSaleCompletionNotificationAsync(int saleId);


        Task<bool> NotifyEmployeeNewLeadAsync(int employeeId, int customerId);
        Task<bool> NotifyManagerSaleCompletionAsync(int saleId);
        Task<bool> NotifyMaintenanceIncidentAsync(int testDriveId);
    }

}
