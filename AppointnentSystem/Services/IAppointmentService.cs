using AppointnentSystem.Models;

namespace AppointnentSystem.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<AppointmentDto> CreateAppointmentAsync(AppointmentDto appointmentDto);
        Task<AppointmentDto> UpdateAppointmentAsync(int Id, AppointmentDto appointmentDto);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
