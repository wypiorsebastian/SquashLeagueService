using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Domain.Repositories;

public interface IMemberRepository
{
    Task AddMember(Member member);
    Task<Member> GetMemberById(Guid memberId);
    Task<Member> GetMemberByIdentityId(string identityId);
    Task<IEnumerable<Member>> GetMembers();
}