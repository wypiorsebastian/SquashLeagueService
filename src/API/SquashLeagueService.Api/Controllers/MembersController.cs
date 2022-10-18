using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashLeagueService.Application.Members.Queries.GetMember;
using SquashLeagueService.Application.Members.Queries.GetMembers;

namespace SquashLeagueService.Api.Controllers;

[Route("api/members")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [Authorize(Policy = "Authenticated")]
    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _mediator.Send(new GetMembersQuery());
        return Ok(members);
    }
    
    [Authorize(Policy = "Authenticated")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(string id)
    {
        var query = new GetMemberQuery(id);
        var result = await _mediator.Send(query);
    
        if (result is null)
            return NotFound("Member with provided identifier does not exist");
        return Ok(result);
    }
    
    // //TODO - change role to Admin
    // [Authorize(Policy = "Authenticated")]
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
    // {
    //     command = command with { UserId = id };
    //     var result = await _mediator.Send(command);
    //     return Ok();
    // }
}