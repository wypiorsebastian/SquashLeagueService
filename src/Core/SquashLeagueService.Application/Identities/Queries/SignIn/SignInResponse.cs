namespace SquashLeagueService.Application.Identities.Queries.SignIn;

public record AuthenticationResponse(string MemberId, string Username, string Email, string FirstName, string LastName, string Token);