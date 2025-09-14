using Application.Validators;
using Domain.Absractions;
using Domain.CoreServices;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories
{
    public class CategoryRepository(AppDBContext appDBContext) : IRepositoty<Category>
    {
        public Task<OperationResult<Guid>> Add(Category entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<Category>> Delete(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<Guid>> Edit(Category entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<List<Category>>> GetAll(CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Category>>();
            var categories = await appDBContext.Categories.Where(category => !category.DeletedAt.HasValue)
           .ToListAsync(cancellationToken);
            if(categories == null) result.StatusCode = Statuses.NotExist;
            else
            {
                result.StatusCode = Statuses.Success;
                result.Data = categories;
            }
            return result;

        }

        public async Task<OperationResult<Category?>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Category?>();
            var category = await appDBContext.Categories.FirstOrDefaultAsync(act => act.Id == id);
            if(category == null)  result.StatusCode = Statuses.NotExist;
            else
            {
                result.StatusCode = Statuses.Success;
                result.Data = category;
            }
            return result;
        }

        public Task<OperationResult<Activity>> GetByIdTest(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
