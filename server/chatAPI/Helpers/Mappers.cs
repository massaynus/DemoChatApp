using AutoMapper;
using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Helpers;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Models.User, DTOs.User>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.StatusName)
            )
            .ForMember(
                dest => dest.Role,
                opt => opt.MapFrom(src => src.Role.RoleName)
            )
            .ReverseMap();

        CreateMap<Models.User, DTOs.UserSignUpResponse>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.StatusName)
            )
            .ReverseMap();

        CreateMap<DTOs.User, DTOs.UserSignUpResponse>();

        CreateMap<DTOs.User, DTOs.UserStatusChangeResponse>();

        CreateMap<DTOs.UserSignUpRequest, Models.User>();
    }
}