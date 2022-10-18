namespace SquashLeagueService.Application.Members.Queries.GetMembers;

public record MemberForListDto
{
    public string Id { get; init; }
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public bool LockoutEnabled { get; init; }
    public bool IsActive { get; init; }
}