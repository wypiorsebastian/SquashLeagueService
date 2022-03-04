namespace SquashLeagueService.Application.Identities.Queries.SignIn;

public record AuthenticationResponse(string Id, string Username, string Email, string Token);