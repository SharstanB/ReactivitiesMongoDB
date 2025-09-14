using Application.Validators;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Repositories
{
    public class CityRepository(AppDBContext appDBContext) : IRepositoty<City>
    {
        public Task<OperationResult<Guid>> Add(City entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<City>> Delete(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<Guid>> Edit(City entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<List<City>>> GetAll(CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<City>>();
            var cities = await appDBContext.Cities.Where(city => !city.DeletedAt.HasValue)
              .ToListAsync(cancellationToken);
            if(cities == null) result.StatusCode = Statuses.NotExist;
            else
            {
                result.StatusCode = Statuses.Success;
                result.Data = cities;
            }
            return result;

        }

        public Task<OperationResult<City?>> GetById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<Activity>> GetByIdTest(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
