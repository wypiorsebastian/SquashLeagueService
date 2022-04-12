using MediatR;

namespace SquashLeagueService.Application.Identities.Queries.GetRoles;

public record GetApplicationRolesQuery() : IRequest<IEnumerable<ApplicationRolesDto>>;

public class GetApplicationRolesQueryHandler : IRequestHandler<GetApplicationRolesQuery, IEnumerable<ApplicationRolesDto>>
{
    public Task<IEnumerable<ApplicationRolesDto>> Handle(GetApplicationRolesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
    