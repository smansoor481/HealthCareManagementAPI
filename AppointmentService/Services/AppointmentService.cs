using AppointmentService.DTOs;
using AppointmentService.Entity;
using AppointmentService.Exceptions;
using AppointmentService.Repository;
using AutoMapper;
using System.Net.Http.Json;

namespace AppointmentService.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public AppointmentService(IAppointmentRepository repository, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<int> GetDoctorIdByUserIdAsync(int userId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<DoctorApiResponse>(
                $"http://localhost:5003/api/Doctor/by-user/{userId}");

            if (response == null || response.Data == null)
                throw new NotFoundException("Doctor profile not found. Admin must add doctor profile with this doctor's login UserId.");

            return response.Data.DoctorId;
        }

        public async Task<string> BookAppointmentAsync(BookAppointmentDto dto, int patientId)
        {
            var appointment = _mapper.Map<Appointment>(dto);

            appointment.PatientId = patientId;
            appointment.Status = AppointmentStatus.Booked;

            await _repository.AddAsync(appointment);

            return "Appointment booked successfully";
        }

        public async Task<string> CancelAppointmentByPatientAsync(int appointmentId, int patientId)
        {
            var appointment = await _repository.GetByIdAsync(appointmentId);

            if (appointment == null)
                throw new NotFoundException("Appointment not found");

            if (appointment.PatientId != patientId)
                throw new ForbiddenException("You can cancel only your own appointment");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new BadRequestException("Completed appointment cannot be cancelled");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new BadRequestException("Appointment is already cancelled");

            appointment.Status = AppointmentStatus.Cancelled;

            await _repository.UpdateAsync(appointment);

            return "Appointment cancelled successfully";
        }

        public async Task<string> CompleteAppointmentByDoctorAsync(int appointmentId, int doctorUserId)
        {
            var doctorId = await GetDoctorIdByUserIdAsync(doctorUserId);

            var appointment = await _repository.GetByIdAsync(appointmentId);

            if (appointment == null)
                throw new NotFoundException("Appointment not found");

            if (appointment.DoctorId != doctorId)
                throw new ForbiddenException("You can complete only your assigned appointment");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new BadRequestException("Cancelled appointment cannot be completed");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new BadRequestException("Appointment is already completed");

            appointment.Status = AppointmentStatus.Completed;

            await _repository.UpdateAsync(appointment);

            return "Appointment completed successfully";
        }

        public async Task<string> CancelAppointmentByDoctorAsync(int appointmentId, int doctorUserId)
        {
            var doctorId = await GetDoctorIdByUserIdAsync(doctorUserId);

            var appointment = await _repository.GetByIdAsync(appointmentId);

            if (appointment == null)
                throw new NotFoundException("Appointment not found");

            if (appointment.DoctorId != doctorId)
                throw new ForbiddenException("You can cancel only your assigned appointment");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new BadRequestException("Completed appointment cannot be cancelled");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new BadRequestException("Appointment is already cancelled");

            appointment.Status = AppointmentStatus.Cancelled;

            await _repository.UpdateAsync(appointment);

            return "Appointment cancelled successfully";
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsAsync(string role, int userId)
        {
            IEnumerable<Appointment> appointments;

            if (role == "Admin")
            {
                appointments = await _repository.GetAllAsync();
            }
            else if (role == "Patient")
            {
                appointments = await _repository.GetByPatientIdAsync(userId);
            }
            else if (role == "Doctor")
            {
                var doctorId = await GetDoctorIdByUserIdAsync(userId);
                appointments = await _repository.GetByDoctorIdAsync(doctorId);
            }
            else
            {
                throw new ForbiddenException("You are not allowed to view appointments");
            }

            return _mapper.Map<IEnumerable<AppointmentResponseDto>>(appointments);
        }
    }
}

    public class DoctorApiResponse
    {
        public bool Success { get; set; }
        public DoctorData? Data { get; set; }
    }

    public class DoctorData
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string? DoctorName { get; set; }
        public string? Specialization { get; set; }
    }
