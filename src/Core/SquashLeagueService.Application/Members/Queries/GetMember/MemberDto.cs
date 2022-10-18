namespace SquashLeagueService.Application.Members.Queries.GetMember;

public record MemberDto
{
    public string Id { get; init; }
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public bool LockoutEnabled { get; init; }
    public DateTimeOffset? LockoutEnd { get; init; }
    public bool IsActive { get; init; }
    public List<MemberRoleDto> Roles { get; init; } = new List<MemberRoleDto>();
}