using DoctorService.DTOs;
using DoctorService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorService service, ILogger<DoctorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-doctor")]
        public async Task<IActionResult> AddDoctor([FromBody] CreateDoctorDto dto)
        {
            _logger.LogInformation("AddDoctor API called");

            var message = await _service.AddDoctorAsync(dto);

            _logger.LogInformation("AddDoctor API completed");

            return Ok(new
            {
                success = true,
                message
            });
        }


        [Authorize(Roles = "Admin,Doctor,Patient")]
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            _logger.LogInformation("GetAllDoctors API called");

            var doctors = await _service.GetAllDoctorsAsync();

            _logger.LogInformation("GetAllDoctors API completed");

            return Ok(new
            {
                success = true,
                data = doctors
            });
        }

        [AllowAnonymous]
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetDoctorByUserId(int userId)
        {
            var doctor = await _service.GetDoctorByUserIdAsync(userId);

            return Ok(new
            {
                success = true,
                data = doctor
            });
        }

        //[Authorize(Roles = "Admin, Doctor")]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDto dto)
        {
            _logger.LogInformation("UpdateDoctor API called. DoctorId: {DoctorId}", id);

            var message = await _service.UpdateDoctorAsync(id, dto);

            _logger.LogInformation("UpdateDoctor API completed. DoctorId: {DoctorId}", id);

            return Ok(new
            {
                success = true,
                message
            });
        }
    }
}