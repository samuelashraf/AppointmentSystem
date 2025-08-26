using AppointnentSystem.Helper;
using System.ComponentModel.DataAnnotations;

namespace AppointnentSystem.Models
{
    public class AppointmentDto
    {
     public int Id {  get; set; } 
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        
        public string  Status { get; set; }
        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
