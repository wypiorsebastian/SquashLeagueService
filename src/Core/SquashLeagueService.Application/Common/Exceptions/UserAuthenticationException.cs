namespace SquashLeagueService.Application.Common.Exceptions;

public class UserAuthenticationException : Exception
{
    public string ErrorMessage { get; set; }
    public ICollection<string> ErrorList = new List<string>();
    
    public UserAuthenticationException(string exceptionMessage)
        : base(exceptionMessage)
    {
        ErrorMessage = exceptionMessage;
        Data.Add("error_code", "user_signin_exception");
    }
}