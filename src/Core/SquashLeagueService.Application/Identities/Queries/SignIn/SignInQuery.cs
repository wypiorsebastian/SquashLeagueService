using MediatR;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Application.Identities.Queries.SignIn;

public record SignInQuery(string Username, string Password) : IRequest<AuthenticationResponse>;

public class SignInQueryHandler : IRequestHandler<SignInQuery, AuthenticationResponse>
{
    private readonly IIdentityService _authenticationService;
    private readonly IMemberRepository _memberRepository;

    public SignInQueryHandler(IIdentityService authenticationService, IMemberRepository memberRepository)
    {
        _authenticationService =
            authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _memberRepository = memberRepository;
    }

    public async Task<AuthenticationResponse> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var identity = await _authenticationService.SignInAsync(request);
        var member = await _memberRepository.GetMemberByIdentityId(identity.IdentityId);

        var authenticationResponse = new AuthenticationResponse(member.Id.ToString(), identity.UserName, identity.Email,
            member.FirstName, member.LastName, identity.Token);
        
        return authenticationResponse;
    }
}
