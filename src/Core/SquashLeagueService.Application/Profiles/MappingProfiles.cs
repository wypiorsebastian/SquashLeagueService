using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Users.Queries.GetUser;
using SquashLeagueService.Application.Users.Queries.GetUsers;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
        CreateMap<ApplicationUser, UserForListDto>();
        CreateMap<IdentityRole, UserRoleDto>().ReverseMap();
    }
}