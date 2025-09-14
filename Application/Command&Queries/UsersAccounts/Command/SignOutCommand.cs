using Application.DataTransferObjects.UsersAccounts;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Enums;
using Domain.Mediator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command_Queries.UsersAccounts.Command
{
    public class SignOutCommand
    {
        public class Command : IRequest<OperationResult<bool>>
        {
            public required string RefreshToken { get; set; }
        }

        public class Handler(IIdentityService identityService
           ) : IRequestHandler<Command, OperationResult<bool>>
        {
            public async Task<OperationResult<bool>> Handle(Command request, CancellationToken cancellationToken = default)
            {
                //var validationResult = await validator.ValidateAsync(request, cancellationToken);
                //if (!validationResult.IsValid)
                //{
                //    return new OperationResult<bool>()
                //    {
                //        ExceptionDetails = (new ValidationException(validationResult.Errors)).Message,
                //        Message = string.Join(",", validationResult.Errors),
                //        StatusCode = Statuses.Exception,
                //    };
                //}
                var result = await identityService.SignoutAsync(request.RefreshToken);

                return result;
            }
        }
    }
}
