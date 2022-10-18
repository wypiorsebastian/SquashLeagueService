using MediatR;
using Microsoft.AspNetCore.Mvc;
using SquashLeagueService.Application.Identities.Commands.Signup;
using SquashLeagueService.Application.Identities.Queries.SignIn;

namespace SquashLeagueService.Api.Controllers;

[Route("api/identity")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Signup")]
    public async Task<ActionResult<SignupResponse>> Signup([FromBody] SignupCommand signupCommand)
    {
        var result = await _mediator.Send(signupCommand);
        return Ok();
    }
    
    [HttpPost("Signin")]
    public async Task<ActionResult<AuthenticationResponse>> Signin([FromBody] SignInQuery signInQuery)
    {
        var result = await _mediator.Send(signInQuery);
        return Ok(result);
    }
}