namespace SquashLeagueService.Application.Users.Queries.GetUser;

public class UserDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool IsActive { get; set; }
    public List<UserRoleDto> Roles { get; set; } = new List<UserRoleDto>();
}