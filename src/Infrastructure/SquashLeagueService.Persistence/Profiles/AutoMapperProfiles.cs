using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace SquashLeagueService.Persistence.Profiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //CreateMap<IdentityRole, UserRoleDto>().ReverseMap();
    }
}