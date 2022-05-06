namespace SquashLeagueService.Application.Users.UpdateUser.DTOs;

public record UpdateUserDto(string Username,
    string FirstName,
    string LastName,
    string Email,
    object Phone,
    IReadOnlyList<UpdateUserRoleDto> UserRoles
);