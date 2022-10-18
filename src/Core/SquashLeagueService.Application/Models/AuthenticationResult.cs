namespace SquashLeagueService.Application.Models;

public sealed record AuthenticationResult(string IdentityId, string UserName, string Email, string Token);
