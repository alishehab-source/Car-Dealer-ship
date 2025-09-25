namespace CarDealershipAPI.DTOs.Employee
{
    public class EmployeeScheduleDto
    {
        public string WorkingHours { get; set; } = string.Empty;
        public List<string> WorkingDays { get; set; } = new();
        public int RemainingAnnualLeave { get; set; }
        public int RemainingSickLeave { get; set; }
        public List<EmployeeLeaveDto>? UpcomingLeaves { get; set; }
        public List<EmployeeAppointmentDto>? UpcomingAppointments { get; set; }
    }

}
