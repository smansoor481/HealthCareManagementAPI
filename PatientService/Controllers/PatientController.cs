using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.DTOs;
using PatientService.Services;

namespace PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientService service, ILogger<PatientController> logger)
        {
            _service = service;
            _logger = logger;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst("UserId")!.Value);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateProfile([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // <-- this shows real error
            }
            var userId = GetUserId();

            _logger.LogInformation("Create patient profile API called. UserId: {UserId}", userId);

            var message = await _service.CreateProfileAsync(dto, userId);

            _logger.LogInformation("Create patient profile API completed. UserId: {UserId}", userId);

            return Ok(new
            {
                success = true,
                message = message
            });
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetUserId();

            _logger.LogInformation("Get my profile API called. UserId: {UserId}", userId);

            var patient = await _service.GetMyProfileAsync(userId);

            _logger.LogInformation("Get my profile API completed. UserId: {UserId}", userId);

            return Ok(new
            {
                success = true,
                data = patient
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPatients()
        {
            _logger.LogInformation("Get all patients API called by Admin");

            var patients = await _service.GetAllPatientsAsync();

            _logger.LogInformation("Get all patients API completed");

            return Ok(new
            {
                success = true,
                data = patients
            });
        }

        [Authorize(Roles = "Patient")]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdatePatientDto dto)
        {
            var userId = GetUserId();

            _logger.LogInformation("Update patient profile API called. UserId: {UserId}", userId);

            var message = await _service.UpdateProfileAsync(dto, userId);

            _logger.LogInformation("Update patient profile API completed. UserId: {UserId}", userId);

            return Ok(new
            {
                success = true,
                message = message
            });
        }
    }
}