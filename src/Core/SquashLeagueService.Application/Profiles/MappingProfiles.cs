using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Identities.Queries.GetRoles;

namespace SquashLeagueService.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CreateMap<ApplicationUser, UserDto>().ReverseMap();
        // CreateMap<ApplicationUser, UserForListDto>();
        // CreateMap<IdentityRole, UserRoleDto>().ReverseMap();
        CreateMap<IdentityRole, ApplicationRoleDto>().ReverseMap();
    }
}