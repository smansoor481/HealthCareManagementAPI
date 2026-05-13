using AppointmentService.DTOs;
using AppointmentService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst("UserId")!.Value);
        }

        private string GetRole()
        {
            return User.FindFirst(ClaimTypes.Role)!.Value;
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentDto dto)
        {
            var patientId = GetUserId();

            var message = await _service.BookAppointmentAsync(dto, patientId);

            return Ok(new
            {
                success = true,
                message
            });
        }

        [Authorize(Roles = "Patient")]
        [HttpPut("patient-cancel/{id}")]
        public async Task<IActionResult> CancelByPatient(int id)
        {
            var patientId = GetUserId();

            var message = await _service.CancelAppointmentByPatientAsync(id, patientId);

            return Ok(new
            {
                success = true,
                message
            });
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("doctor-complete/{id}")]
        public async Task<IActionResult> CompleteByDoctor(int id)
        {
            var doctorId = GetUserId();

            var message = await _service.CompleteAppointmentByDoctorAsync(id, doctorId);

            return Ok(new
            {
                success = true,
                message
            });
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("doctor-cancel/{id}")]
        public async Task<IActionResult> CancelByDoctor(int id)
        {
            var doctorId = GetUserId();

            var message = await _service.CancelAppointmentByDoctorAsync(id, doctorId);

            return Ok(new
            {
                success = true,
                message
            });
        }

        [Authorize(Roles = "Patient,Doctor,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var userId = GetUserId();
            var role = GetRole();

            var appointments = await _service.GetAppointmentsAsync(role, userId);

            return Ok(new
            {
                success = true,
                data = appointments
            });
        }
    }
}