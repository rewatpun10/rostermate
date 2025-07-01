using AutoMapper;
using RosterMate.Application.DTOs;
using RosterMate.Domain.Entities;

namespace RosterMate.Application.Mappings;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        //DTO -> Entity
        CreateMap<CreateStaffDto, Staff>();
        CreateMap<UpdateStaffDto, Staff>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<Staff, StaffDto>();

        CreateMap<EmploymentDetailDto, EmploymentDetail>().ReverseMap();
        CreateMap<PayrollDetailDto, PayrollDetail>().ReverseMap();
    }
}