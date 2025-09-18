using Domain.Absractions;
using Domain.CoreServices;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Services.DBServices;

namespace Application.Repositories
{
    public class ActivityRepository : Repository<Activity>, IRepositoty<Activity>
    {
        public ActivityRepository(MongoDBService appDBContext) : base(appDBContext)
        {
        }

        public async Task<OperationResult<Guid>> Add(Activity activity, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Guid>();
            _appDBContext.Activities.Add(activity); // review why to not use AddAsync
            result = await SaveDataChanges<Guid>(activity, cancellationToken);
            result.Data = result.IsSuccess() ? activity.Id : default;
            return result;
        }


        public async Task<OperationResult<Guid>> Edit(Activity activity, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Guid>();

            var activityToEdit = await _appDBContext.Activities.FirstOrDefaultAsync(act => act.Id == activity.Id);

            if (activityToEdit == null)
            {
                result.StatusCode = Statuses.NotExist;
            }
            else
            {
                activityToEdit.Description = activity.Description;
                activityToEdit.Date = activity.Date.ToUniversalTime();
                activityToEdit.Title = activity.Title;
                activityToEdit.IsCancelled = activity.IsCancelled;
                activityToEdit.Venue = activity.Venue;
                activityToEdit.Latitude = activity.Latitude;
                activityToEdit.Longitude = activity.Longitude;
                activityToEdit.Category = activity.Category;
                activityToEdit.City = activity.City;
                _appDBContext.Activities.Update(activityToEdit);
                result =  await SaveDataChanges<Guid>(activity, cancellationToken);
            }

            return result;
        }
        public async Task<OperationResult<List<Activity>>> GetAll(CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Activity>>();
            var activities = await _appDBContext.Activities.Include(a => a.Category)
                .Where(activity => !activity.DeletedAt.HasValue)
                .OrderBy(activity => activity.CreatedAt)
                .ToListAsync(cancellationToken);
            if (activities == null) result.StatusCode = Statuses.NotExist;
            else
            {
                result.Data = activities;
                result.StatusCode = Statuses.Success;
            }
            return result;
        }
        public  async Task<OperationResult<Activity>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Activity>();
            var activity = await _appDBContext.Activities.Include(a => a.Category)
                .FirstOrDefaultAsync(act => act.Id == id);
            if (activity == null) result.StatusCode = Statuses.NotExist;
            else
            {
                result.Data = activity;
                result.StatusCode = Statuses.Success;
            }
            return result;
        }
        public async Task<OperationResult<Activity>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Activity>();
            var activity = await _appDBContext.Activities.Include(a => a.City).Include(a => a.Category)
                .FirstOrDefaultAsync(act => act.Id == id);
            if (activity == null) result.StatusCode = Statuses.NotExist;
            else
            {
                activity.DeletedAt = DateTime.UtcNow;
                _appDBContext.Activities.Update(activity);
                result = await SaveDataChanges<Activity>(activity, cancellationToken);
            }
            return result;
        }
   

        public async Task<OperationResult<Activity>> GetByIdTest(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Activity>();

            var activity = await _appDBContext.Activities.Include(a => a.City).Include(a => a.Category)
                .FirstOrDefaultAsync(act => act.Id == id);
            if(activity == null) result.StatusCode = Statuses.NotExist;
            else
            {
                result.Data = activity;
                result.StatusCode = Statuses.Success;
            }
            return result;
        }
    }
}
