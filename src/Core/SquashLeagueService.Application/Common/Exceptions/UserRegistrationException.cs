using Microsoft.AspNetCore.Identity;

namespace SquashLeagueService.Application.Common.Exceptions;

public class UserRegistrationException : Exception
{
    public string ErrorMessage { get; set; }
    public ICollection<string> ErrorList = new List<string>();
    
    public UserRegistrationException(string exceptionMessage, IEnumerable<IdentityError> errors)
        : base(exceptionMessage)
    {
        ErrorMessage = exceptionMessage;
        Data.Add("error_code", "user_registration_exception");

        foreach (var error in errors)
        {
            ErrorList.Add(error.Description);
        }
    }
}