using Application.UsersAccounts.Command;
using FluentValidation;

namespace Application.Validators
{
    public class SignupValidator : AbstractValidator<SignUpCommand.Command>
    {
        public SignupValidator()
        {
            RuleFor(x => x.Signup.Email).NotEmpty().WithMessage("The email is required, should not be empty...");
            RuleFor(x => x.Signup.DisplayName).NotEmpty().WithMessage("The Name is required, should not be empty..");
            RuleFor(x => x.Signup.DOB).NotEmpty().WithMessage("The Date of birth is required, should bot be empty..");
        }
    }
}
