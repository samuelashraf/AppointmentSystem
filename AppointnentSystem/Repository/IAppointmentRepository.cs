using AppointnentSystem.Models;

namespace AppointnentSystem.Repository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentAsync(int Id, Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
