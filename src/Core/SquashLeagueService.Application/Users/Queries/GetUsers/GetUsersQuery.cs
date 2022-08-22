using AutoMapper;
using MediatR;
using SquashLeagueService.Persistence.Repositories.UnitOfWork;

namespace SquashLeagueService.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<IEnumerable<UserForListDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserForListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<UserForListDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetApplicationUsers();
        return _mapper.Map<IEnumerable<UserForListDto>>(users);
    }
}