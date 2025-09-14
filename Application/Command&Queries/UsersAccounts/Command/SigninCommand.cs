using Application.DataTransferObjects.UsersAccounts;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Enums;
using Domain.Mediator;
using FluentValidation;


namespace Application.UsersAccounts.Command
{
    public class SignInCommand
    {
        public class Command : IRequest<OperationResult<string>>
        {
            public required SigninDTO Login { get; set; }
        }

        public class Handler(IIdentityService identityService
           , IValidator<Command> validator
           ) : IRequestHandler<Command, OperationResult<string>>
        {
            public async Task<OperationResult<string>> Handle(Command request, CancellationToken cancellationToken = default)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return new OperationResult<string>()
                    {
                        ExceptionDetails = (new ValidationException(validationResult.Errors)).Message,
                        Message = string.Join(",", validationResult.Errors),
                        StatusCode = Statuses.Exception,
                    };
                }
                var result = await identityService.SigninAsync(request.Login.Email, request.Login.Password);

                return result;
            }
        }
    }
}
