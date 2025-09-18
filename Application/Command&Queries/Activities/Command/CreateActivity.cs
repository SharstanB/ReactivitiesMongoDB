using Domain.Entities;
using Domain.Mediator;
using Application.DataTransferObjects.Activity;
using FluentValidation;
using Domain.Enums;
using Domain.CoreServices;
using Domain.Absractions;

namespace Application.Activities.Command
{
    public class CreateActivity
    {
        public class Command : IRequest<OperationResult<Guid>>
        {
           public required  CreateActivityDTO Activity { get; set; }

        }

        public class Handler(IRepositoty<Activity> activityRepositoty,
            IValidator<Command> validator) : IRequestHandler<Command, OperationResult<Guid>>
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

                var Activity = new Activity()
                {
                    City = request.Activity.City,
                    Venue = request.Activity.Venue,
                    Latitude = request.Activity.Latitude,
                    Longitude = request.Activity.Longitude,
                    Category = new Category { Name = request.Activity.Category },
                    Description = request.Activity.Description,
                    Title = request.Activity.Title,
                    Date = request.Activity.Date.ToUniversalTime(),
                };
                var result = await activityRepositoty.Add(Activity, cancellationToken);

                return result;
            }
             
        }
    }
}
