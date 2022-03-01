﻿namespace SquashLeagueService.Application.Common.Exceptions;

public class RoleRegistrationException : Exception
{
    public string ErrorMessage { get; set; }
    public ICollection<string> ErrorList = new List<string>();
    
    public RoleRegistrationException(string exceptionMessage)
        : base(exceptionMessage)
    {
        ErrorMessage = exceptionMessage;
        Data.Add("error_code", "role_registration_exception");
    }
}