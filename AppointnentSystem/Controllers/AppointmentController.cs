using AppointnentSystem.Models;
using AppointnentSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointnentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("GetAllAppointment")]
        public async Task<IActionResult> GetAllAppointment()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();
                if (appointments == null || !appointments.Any())

                    return Ok(new List<AppointmentDto>());

                return Ok(appointments);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAppointmentById/{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                    return NotFound($"This Appointment not found.");

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                if (appointmentDto == null)
                    return BadRequest("Appointment data is null.");

                var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointmentDto);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointment.Id }, createdAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                    return NotFound($"This Appointment not found.");

                var result = await _appointmentService.DeleteAppointmentAsync(id);
                if (!result)
                    return StatusCode(500, "A problem happened while handling your request.");

              return Ok("Appointment deleted successfully.");
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                if (appointmentDto == null)
                    return BadRequest("Appointment data is null.");

                var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                    return NotFound($"This Appointment not found.");

                var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(id, appointmentDto);
                if (updatedAppointment == null)
                    return StatusCode(500, "A problem happened while handling your request.");

                return Ok(updatedAppointment);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
