using FluentValidation;

namespace SquashLeagueService.Application.Identities.Commands.Signup;

public class SignupCommandValidator : AbstractValidator<SignupCommand>
{
    public SignupCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .NotEmpty();
    }
    
}