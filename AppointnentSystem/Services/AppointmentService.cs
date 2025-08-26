using AppointnentSystem.Helper;
using AppointnentSystem.Models;
using AppointnentSystem.Repository;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;

namespace AppointnentSystem.Services
{
    public class AppointmentService:IAppointmentService
    {
        public readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            return appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                CustomerName = a.CustomerName,
                AppointmentDate = a.AppointmentDate,
                Notes = a.Notes,
                 Status = a.Status.ToString()
            }).ToList();

        }


        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return null;
            return new AppointmentDto
            {
                Id = appointment.Id,
                CustomerName = appointment.CustomerName,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes
            };
        }
        public async Task<AppointmentDto> CreateAppointmentAsync(AppointmentDto appointmentDto)

        {
            if (appointmentDto.AppointmentDate <= DateTime.Now)
                throw new ArgumentException("Appointment date cannot be in the past");
            if (!Enum.TryParse<StatusAppointment>(appointmentDto.Status.ToString(), true, out var status))
                throw new ArgumentException("Invalid status");

            var ExistingAppointments = await _appointmentRepository.GetAllAppointmentsAsync();
            bool IsDuplicate = ExistingAppointments.Any(a => a.CustomerName == appointmentDto.CustomerName && a.AppointmentDate == appointmentDto.AppointmentDate);
            if (IsDuplicate)
                throw new Exception("An appointment already exists at the specified date and time.");

            var appointment =new Appointment
           {
               CustomerName = appointmentDto.CustomerName,
               AppointmentDate = appointmentDto.AppointmentDate,
               Status = status,
               Notes = appointmentDto.Notes
           };
            await _appointmentRepository.CreateAppointmentAsync(appointment);
            appointmentDto.Id = appointment.Id;
            return appointmentDto;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return false;
        await _appointmentRepository.DeleteAppointmentAsync(id);
            return true;
        }

        

        public async Task<AppointmentDto> UpdateAppointmentAsync(int Id, AppointmentDto appointmentDto)
        {
           var appointment= await _appointmentRepository.GetAppointmentByIdAsync(Id);
            if (appointment == null)
                throw new Exception("Appointment not found");
;
            if (appointmentDto.AppointmentDate < DateTime.Now)
                throw new ArgumentException("Appointment date cannot be in the past");
            if (!Enum.TryParse<StatusAppointment>(appointmentDto.Status.ToString(), true, out var status))
                throw new ArgumentException("Invalid status");
            var existingAppointments = await _appointmentRepository.GetAllAppointmentsAsync();
            bool isDuplicate = existingAppointments.Any(a => a.Id != Id && a.CustomerName == appointmentDto.CustomerName && a.AppointmentDate == appointmentDto.AppointmentDate);
            if (isDuplicate)
                throw new Exception("An appointment already exists at the specified date and time");

            if (appointment.Status != StatusAppointment.Scheduled)
                throw new ArgumentException("Only scheduled appointments can be updated");

            appointment.CustomerName = appointmentDto.CustomerName;
            appointment.AppointmentDate = appointmentDto.AppointmentDate;
            appointment.Status = status ;
            appointment.Notes = appointmentDto.Notes;
            
            var updatedAppointment = await _appointmentRepository.UpdateAppointmentAsync(Id, appointment);
            if (updatedAppointment == null)
                return null;
            appointmentDto.Id = updatedAppointment.Id;
            return appointmentDto;
        }
    }
}
