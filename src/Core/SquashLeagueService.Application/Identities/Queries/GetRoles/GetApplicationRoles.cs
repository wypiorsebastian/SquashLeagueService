using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Persistence.Repositories.UnitOfWork;

namespace SquashLeagueService.Application.Identities.Queries.GetRoles;

public record GetApplicationRolesQuery() : IRequest<IEnumerable<ApplicationRoleDto>>;

public class GetApplicationRolesQueryHandler : IRequestHandler<GetApplicationRolesQuery, IEnumerable<ApplicationRoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetApplicationRolesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IEnumerable<ApplicationRoleDto>> Handle(GetApplicationRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.IdentityRepository.GetApplicationRoles();

        return _mapper.Map<IEnumerable<ApplicationRoleDto>>(roles);
    }
}
    