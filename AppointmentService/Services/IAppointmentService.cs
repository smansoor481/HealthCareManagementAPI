using AppointmentService.DTOs;

namespace AppointmentService.Services
{
    public interface IAppointmentService
    {
        Task<string> BookAppointmentAsync(BookAppointmentDto dto, int patientId);
        Task<string> CancelAppointmentByPatientAsync(int appointmentId, int patientId);
        Task<string> CompleteAppointmentByDoctorAsync(int appointmentId, int doctorId);
        Task<string> CancelAppointmentByDoctorAsync(int appointmentId, int doctorId);
        Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsAsync(string role, int userId);
    }
}
