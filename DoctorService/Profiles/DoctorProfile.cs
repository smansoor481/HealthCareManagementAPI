using AutoMapper;
using DoctorService.DTOs;
using DoctorService.Entity;

namespace DoctorService.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            // DTO to Entity
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>();

            // Entity to DTO
            CreateMap<Doctor, DoctorResponseDto>();
        }
    }
}
