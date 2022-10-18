using AutoMapper;
using MediatR;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Application.Members.Queries.GetMember;

public sealed record GetMemberQuery(string MemberId) : IRequest<MemberDto>;

public sealed class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, MemberDto>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IIdentityService _identityService;

    public GetMemberQueryHandler(IMemberRepository memberRepository, IIdentityService identityService)
    {
        _memberRepository = memberRepository;
        _identityService = identityService;
    }

    public async Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetMemberById(Guid.Parse(request.MemberId));
        var identityData = await _identityService.GetIdentityData(member.IdentityUserId);
        var memberRoles = await _identityService.GetIdentityRoles(member.IdentityUserId);

        var memberDto = new MemberDto()
        {
            Id = member.Id.ToString(),
            Email = identityData.Email,
            Roles = memberRoles.Select(x => new MemberRoleDto(x, x)).ToList(),
            Username = identityData.UserName,
            FirstName = member.FirstName,
            LastName = member.LastName,
            PhoneNumber = identityData.PhoneNumber
        };

        return memberDto;
    }
}