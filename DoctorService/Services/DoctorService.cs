using AutoMapper;
using DoctorService.DTOs;
using DoctorService.Entity;
using DoctorService.Exceptions;
using DoctorService.Repository;

namespace DoctorService.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IDoctorRepository repository, IMapper mapper, ILogger<DoctorService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> AddDoctorAsync(CreateDoctorDto dto)
        {
            _logger.LogInformation("AddDoctor service started");

            var existingDoctor = await _repository.GetByUserIdAsync(dto.UserId);
            if (existingDoctor != null)
            {
                throw new NotFoundException("Doctor profile already exists for this login UserId");
            }

            var doctor = _mapper.Map<Doctor>(dto);

            await _repository.AddAsync(doctor);

            _logger.LogInformation("Doctor added successfully. DoctorId: {DoctorId}", doctor.DoctorId);

            return "Doctor added successfully";
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllDoctorsAsync()
        {
            _logger.LogInformation("GetAllDoctors service started");

            var doctors = await _repository.GetAllAsync();

            _logger.LogInformation("GetAllDoctors service completed");

            return _mapper.Map<IEnumerable<DoctorResponseDto>>(doctors);
        }

        public async Task<DoctorResponseDto> GetDoctorByUserIdAsync(int userId)
        {
            _logger.LogInformation("GetDoctorByUserId service started. UserId: {UserId}", userId);

            var doctor = await _repository.GetByUserIdAsync(userId);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor profile not found for this login UserId");
            }

            return _mapper.Map<DoctorResponseDto>(doctor);
        }

        public async Task<string> UpdateDoctorAsync(int id, UpdateDoctorDto dto)
        {
            _logger.LogInformation("UpdateDoctor service started. DoctorId: {DoctorId}", id);

            var existingDoctor = await _repository.GetByIdAsync(id);

            if (existingDoctor == null)
            {
                _logger.LogWarning("Doctor not found in service. DoctorId: {DoctorId}", id);
                throw new NotFoundException("Doctor not found");
            }

            _mapper.Map(dto, existingDoctor);

            await _repository.UpdateAsync(existingDoctor);

            _logger.LogInformation("Doctor updated successfully. DoctorId: {DoctorId}", id);

            return "Doctor updated successfully";
        }
    }
}