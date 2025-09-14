using Application.DataTransferObjects.UsersAccounts;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Enums;
using Domain.Mediator;
using FluentValidation;

namespace Application.UsersAccounts.Command
{
    public class SignUpCommand
    {
        public class Command : IRequest<OperationResult<Guid>>
        {
            public required SignupDTO Signup { get; set; }

        }

        public class Handler(IIdentityService identityService
            ,IValidator<Command> validator
            )  : IRequestHandler<Command, OperationResult<Guid>>
        {
            public async Task<OperationResult<Guid>> Handle(Command request, CancellationToken cancellationToken = default)
            {
                //TODO  I should find a way for this garbage 
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return new OperationResult<Guid>()
                    {
                        ExceptionDetails = (new ValidationException(validationResult.Errors)).Message,
                        Message = string.Join(",", validationResult.Errors),
                        StatusCode = Statuses.Exception,
                    };
                }
                var result = await identityService.SignupAsync(request.Signup.Email,
                    request.Signup.Password,
                    request.Signup.Email,
                    request.Signup.DisplayName,
                    request.Signup.DOB);

                return result;
            }

        }
    }
}
