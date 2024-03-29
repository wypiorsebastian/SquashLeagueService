﻿using SquashLeagueService.Domain.Exceptions;

namespace SquashLeagueService.Application.Common.Exceptions;

public class UserAlreadyExistsException : DomainExceptionBase
{
    public override string Code => "user_already_exists";

    public UserAlreadyExistsException(string username) : base($"Username: {username} is already registered")
    {}
}