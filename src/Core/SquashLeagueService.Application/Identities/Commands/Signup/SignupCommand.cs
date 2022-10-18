using MediatR;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Application.Identities.Commands.Signup;

public class SignupCommand : IRequest<SignupResponse>
{
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string[] Roles { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
}

public class SignupQueryHandler : IRequestHandler<SignupCommand, SignupResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IMemberRepository _memberRepository;

    public SignupQueryHandler(IIdentityService identityService, IMemberRepository memberRepository)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
    }

    public async Task<SignupResponse> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var signupResult = await _identityService.SignupAsync(request);
        
        var identityId = signupResult.Id;
        var member = Member.Create(request.FirstName, request.LastName, identityId);

        await _memberRepository.AddMember(member);

        return new SignupResponse { Id = signupResult.Id};
    }

    
}

