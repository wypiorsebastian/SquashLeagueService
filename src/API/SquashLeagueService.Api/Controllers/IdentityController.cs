﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using SquashLeagueService.Application.Identities.Commands.Signup;

namespace SquashLeagueService.Api.Controllers;

[Route("api/[controller]")]
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
}