using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashLeagueService.Application.Users.Queries.GetUser;
using SquashLeagueService.Application.Users.Queries.GetUsers;

namespace SquashLeagueService.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Policy = "Authenticated")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    [Authorize(Policy = "Authenticated")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var query = new GetUserQuery(id);
        var result = await _mediator.Send(query);

        if (result is null)
            return NotFound("User with provided identifier does not exist");
        return Ok(result);
    }
    
    //TODO - change trole to Admin
    [Authorize(Policy = "Authenticated")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] Object any)
    {
        return Ok();
    }
}