using AutoMapper;
using MediatR;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Application.Members.Queries.GetMember;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Application.Members.Queries.GetMembers;

public record GetMembersQuery() : IRequest<IEnumerable<MemberForListDto>>;

public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<MemberForListDto>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IIdentityService _identityService;

    public GetMembersQueryHandler(IMemberRepository memberRepository, IIdentityService identityService)
    {
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    public async Task<IEnumerable<MemberForListDto>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetMembers();
        var membersList = await CreateMembersList(members);

        return membersList;
    }

    private async Task<IEnumerable<MemberForListDto>> CreateMembersList(IEnumerable<Member> members)
    {
        var membersList = new System.Collections.Generic.List<MemberForListDto>();

        foreach (var member in members)
        {
            var identityData = await _identityService.GetIdentityData(member.IdentityUserId);
            var memberRoles = await _identityService.GetIdentityRoles(member.IdentityUserId);

            var memberDto = new MemberForListDto()
            {
                Id = member.Id.ToString(),
                Email = identityData.Email,
                Username = identityData.UserName,
                FirstName = member.FirstName,
                LastName = member.LastName,
                PhoneNumber = identityData.PhoneNumber
            };
            
            membersList.Add(memberDto);
        }

        return membersList;
    }
}