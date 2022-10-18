using Microsoft.EntityFrameworkCore;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Persistence.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public MemberRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public async Task AddMember(Member member)
    {
        _applicationDbContext.Add(member);
        await _applicationDbContext.SaveChangesAsync();
    }

    public Task<Member> GetMemberById(Guid memberId)
    {
        return _applicationDbContext.Members.SingleOrDefaultAsync(x => x.Id == memberId);
    }

    public Task<Member> GetMemberByIdentityId(string identityId)
    {
        return _applicationDbContext.Members.SingleOrDefaultAsync(x => x.IdentityUserId == identityId);
    }

    public async Task<IEnumerable<Member>> GetMembers()
    {
        return await _applicationDbContext.Members.ToListAsync();
    }
}