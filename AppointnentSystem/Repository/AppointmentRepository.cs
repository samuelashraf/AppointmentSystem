using AppointnentSystem.Data;
using AppointnentSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace AppointnentSystem.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DbAppontment _context;
        public AppointmentRepository(DbAppontment context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
          return await _context.Appointments.ToListAsync();
        }
        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
              await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            Appointment appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
                return false;
          _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;

        }

        

      

        public async Task<Appointment> UpdateAppointmentAsync(int Id, Appointment appointment)
        {

            Appointment AppointmentUpdate = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == Id);
            if (AppointmentUpdate == null)
                return null;
            AppointmentUpdate.CustomerName = appointment.CustomerName;
            AppointmentUpdate.AppointmentDate = appointment.AppointmentDate;
            AppointmentUpdate.Status = appointment.Status;
            AppointmentUpdate.Notes = appointment.Notes;
            
            await _context.SaveChangesAsync();
            return AppointmentUpdate;


        }
        

    }
}
