using AutoMapper;
using PatientService.DTOs;
using PatientService.Entity;
using PatientService.Exceptions;
using PatientService.Repository;

namespace PatientService.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepository repository, IMapper mapper, ILogger<PatientService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> CreateProfileAsync(CreatePatientDto dto, int userId)
        {
            _logger.LogInformation("Create profile service started. UserId: {UserId}", userId);

            var existingProfile = await _repository.GetByUserIdAsync(userId);

            if (existingProfile != null)
            {
                _logger.LogWarning("Patient profile already exists. UserId: {UserId}", userId);
                throw new BadRequestException("Patient profile already exists");
            }

            var patient = _mapper.Map<Patient>(dto);
            patient.UserId = userId;

            await _repository.AddAsync(patient);

            _logger.LogInformation("Patient profile created successfully. PatientId: {PatientId}, UserId: {UserId}",
                patient.PatientId,
                userId
            );

            return "Patient profile created successfully";
        }

        public async Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync()
        {
            _logger.LogInformation("Get all patients service started");

            var patients = await _repository.GetAllAsync();

            _logger.LogInformation("Get all patients service completed");

            return _mapper.Map<IEnumerable<PatientResponseDto>>(patients);
        }

        public async Task<PatientResponseDto> GetMyProfileAsync(int userId)
        {
            _logger.LogInformation("Get my profile service started. UserId: {UserId}", userId);

            var patient = await _repository.GetByUserIdAsync(userId);

            if (patient == null)
            {
                _logger.LogWarning("Patient profile not found in service. UserId: {UserId}", userId);
                throw new NotFoundException("Patient profile not found");
            }

            _logger.LogInformation("Patient profile fetched successfully. PatientId: {PatientId}, UserId: {UserId}",
                patient.PatientId,
                userId
            );

            return _mapper.Map<PatientResponseDto>(patient);
        }

        public async Task<string> UpdateProfileAsync(UpdatePatientDto dto, int userId)
        {
            _logger.LogInformation("Update profile service started. UserId: {UserId}", userId);

            var existingPatient = await _repository.GetByUserIdAsync(userId);

            if (existingPatient == null)
            {
                _logger.LogWarning("Update failed. Patient profile not found. UserId: {UserId}", userId);
                throw new NotFoundException("Patient profile not found");
            }

            _mapper.Map(dto, existingPatient);

            await _repository.UpdateAsync(existingPatient);

            _logger.LogInformation("Patient profile updated successfully. PatientId: {PatientId}, UserId: {UserId}",
                existingPatient.PatientId,
                userId
            );

            return "Patient profile updated successfully";
        }
    }
}