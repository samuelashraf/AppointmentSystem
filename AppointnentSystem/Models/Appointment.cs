using AppointnentSystem.Helper;

namespace AppointnentSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public StatusAppointment Status { get; set; }= StatusAppointment.Scheduled;
        public string Notes { get; set; }
    }
}
