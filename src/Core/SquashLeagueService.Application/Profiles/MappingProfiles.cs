using AutoMapper;
using SquashLeagueService.Application.Users.Queries.GetUsers;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}