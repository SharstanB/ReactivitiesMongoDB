using Application.DataTransferObjects.UsersAccounts;
using Application.UsersAccounts.Command;
using FluentValidation;

namespace Application.Validators
{
   public class SigninValidator : AbstractValidator<SignInCommand.Command>
    {
        public SigninValidator()
        {
            RuleFor(x => x.Login.Email).NotEmpty().WithMessage("The username is required");
            RuleFor(x => x.Login.Password).NotEmpty().WithMessage("The password is required");
        }
    }
}
