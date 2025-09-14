using Application.DataTransferObjects.Activity;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Entities;
using Domain.Mediator;

namespace Application.Activities.Queries
{
    public class GetActivitiesList
    {

        public class Query : IRequest<OperationResult<List<GetActivitiesDTO>>> { }

        public class Handler(IRepositoty<Activity> activityRepositoty) : IRequestHandler<Query, OperationResult<List<GetActivitiesDTO>>>
        {
            public async Task<OperationResult<List<GetActivitiesDTO>>> Handle(Query request, CancellationToken cancellationToken = default)
            {
                var activities = (await activityRepositoty.GetAll(cancellationToken));

                return new OperationResult<List<GetActivitiesDTO>>()
                {
                    Data = activities.Data.Select(activity => new GetActivitiesDTO()
                    {
                        City = activity.City,
                        CategoryName = activity.Category.Name,
                        CategoryId = activity.CategoryId.ToString(),
                        Date = activity.Date,
                        Description = activity.Description,
                        Id = activity.Id.ToString(),
                        Title = activity.Title,
                        Venue = activity.Venue,
                        Latitude = activity.Latitude,
                        Longitude = activity.Longitude,
                    }).ToList(),
                    Message = activities.Message,
                    StatusCode = activities.StatusCode,
                };
            }
        }
    }
}