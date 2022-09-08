using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Application.Users.Commands.UpdateUser.DTOs;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Domain.Repositories;
using SquashLeagueService.Persistence.Repositories.UnitOfWork;

namespace SquashLeagueService.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    string? UserId,
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    IReadOnlyList<string> UserRoles
) : IRequest;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetApplicationUser(request.UserId);

        if (user is null)
            throw new UserNotFoundException(request.UserId);

        user.FirstName = request.FirstName;
        user.Email = request.Email;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.UserName = request.Username;

        var identityUser = await _userManager.FindByIdAsync(request.UserId);
        var userRoles = await _userManager.GetRolesAsync(identityUser);
        var applicationRoles = await _roleManager.Roles.ToListAsync();

        await _userManager.RemoveFromRolesAsync(identityUser, applicationRoles.Select(x => x.Name));
        await _userManager.AddToRolesAsync(identityUser, request.UserRoles);


        await _unitOfWork.CompleteAsync();
        return Unit.Value;
    }
}