using AutoMapper;
using PatientService.DTOs;
using PatientService.Entity;

namespace PatientService.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>();
            CreateMap<Patient, PatientResponseDto>();
        }
    }
}
