using AutoMapper;
using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Helpers;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<User, UserData>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.StatusName)
            )
            .ForMember(
                dest => dest.Role,
                opt => opt.MapFrom(src => src.Role.RoleName)
            )
            .ReverseMap();

        CreateMap<User, UserSignUpResponse>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.StatusName)
            )
            .ReverseMap();

        CreateMap<UserData, UserSignUpResponse>();

        CreateMap<UserData, UserStatusChangeResponse>();

        CreateMap<UserSignUpRequest, User>();
    }
}