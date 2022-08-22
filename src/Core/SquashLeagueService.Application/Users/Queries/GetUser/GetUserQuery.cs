using AutoMapper;
using MediatR;
using SquashLeagueService.Persistence.Repositories.UnitOfWork;

namespace SquashLeagueService.Application.Users.Queries.GetUser;

public record GetUserQuery(string UserId) : IRequest<UserDto>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetApplicationUser(request.UserId);
        var userRoles = await _unitOfWork.UserRepository.GetUserRoles(request.UserId);

        var userDto = _mapper.Map<UserDto>(user);
        foreach (var userRole in userRoles)
        {
            userDto.Roles.Add(new UserRoleDto(userRole.Id, userRole.Name));
        }
        
        return userDto;
    }
}