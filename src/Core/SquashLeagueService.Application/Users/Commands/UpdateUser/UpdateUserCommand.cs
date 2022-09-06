using System.Text.Json.Serialization;
using MediatR;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Application.Users.Commands.UpdateUser.DTOs;
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
    IReadOnlyList<UpdateUserRoleDto> UserRoles
) : IRequest;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await  _unitOfWork.UserRepository.GetApplicationUser(request.UserId);

        if (user is null)
            throw new UserNotFoundException(request.UserId);

        user.FirstName = request.FirstName;
        user.Email = request.Email;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.UserName = request.Username;
        

        await _unitOfWork.CompleteAsync();
        return  Unit.Value;
    }
}