using AppointmentService.DTOs;
using AppointmentService.Entity;
using AutoMapper;

namespace AppointmentService.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<BookAppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentResponseDto>();
        }
    }
}
