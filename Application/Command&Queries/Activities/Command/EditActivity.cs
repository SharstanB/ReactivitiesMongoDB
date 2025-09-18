using Application.DataTransferObjects.Activity;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Entities;
using Domain.Mediator;

namespace Application.Activities.Command
{
    public class EditActivity
    {
        public class Command : IRequest<OperationResult<Guid>>
        {
            public required EditActivityDTO Activity { get; set; }
        }
        public class Handler(IRepositoty<Activity> activityRepositoty) : IRequestHandler<Command, OperationResult<Guid>>
        {
            public async Task<OperationResult<Guid>> Handle(Command command, CancellationToken cancellationToken = default)
            {
                var Activity = new Activity()
                {
                    City = command.Activity.City,
                    Venue = command.Activity.Venue,
                    Latitude = command.Activity.Latitude,
                    Longitude = command.Activity.Longitude,
                    Category = new Category { Name = command.Activity.Category },
                    Description = command.Activity.Description,
                    Title = command.Activity.Title,
                    Date = command.Activity.Date,
                    Id = command.Activity.Id,
                };
                var result =  await activityRepositoty.Edit(Activity, cancellationToken);
                return result;
            }
        }
    }
}
