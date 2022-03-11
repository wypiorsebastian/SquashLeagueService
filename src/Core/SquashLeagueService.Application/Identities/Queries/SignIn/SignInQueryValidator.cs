using FluentValidation;

namespace SquashLeagueService.Application.Identities.Queries.SignIn;

public class SignInQueryValidator : AbstractValidator<SignInQuery>
{
    public SignInQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}