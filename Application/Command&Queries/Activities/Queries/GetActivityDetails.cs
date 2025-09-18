using Application.DataTransferObjects.Activity;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Entities;
using Domain.Mediator;

namespace Application.Activities.Queries
{
    public class GetActivityDetails
    {
        public class Query : IRequest<OperationResult<GetActivitiesDTO>>
        {
            public Guid Id { get; set; }
        }

        public class Hanlder (IRepositoty<Activity> activityRepositoty) : IRequestHandler<Query, OperationResult<GetActivitiesDTO>>
        {
            public async Task<OperationResult<GetActivitiesDTO>> Handle(Query request, CancellationToken cancellationToken = default)
            {
                var result = await activityRepositoty.GetById(request.Id, cancellationToken);
                var data = result.Data;
                return new OperationResult<GetActivitiesDTO>()
                {
                    Data = new GetActivitiesDTO()
                    {
                        City = data.City,
                        Category = data.Category.Name,
                        Date = data.Date,
                        Description = data.Description,
                        Id = data.Id.ToString(),
                        Title = data.Title,
                        Venue = data.Venue,
                        Latitude = data.Latitude,
                        Longitude = data.Longitude,
                    },
                    Message = result.Message,
                    StatusCode = result.StatusCode,
                };
            }
        }
    }
}
