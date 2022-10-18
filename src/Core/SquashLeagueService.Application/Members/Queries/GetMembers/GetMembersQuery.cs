using AutoMapper;
using MediatR;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Application.Members.Queries.GetMembers;

public record GetMembersQuery() : IRequest<IEnumerable<MemberForListDto>>;

public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<MemberForListDto>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetMembersQueryHandler(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<MemberForListDto>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}