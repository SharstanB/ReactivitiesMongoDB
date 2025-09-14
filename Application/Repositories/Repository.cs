using Domain.Absractions;
using Domain.CoreServices;
using Domain.Enums;
using Persistence;

namespace Application.Repositories
{
    public class Repository<T>
        where T : class, ISoftDeletable
    {
        protected readonly AppDBContext _appDBContext;
        public Repository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task<OperationResult<TResponse>> SaveDataChanges<TResponse>( T entity, CancellationToken
           cancellationToken)
        {
            var operationResult = new OperationResult<TResponse>();
            try
            {
                await _appDBContext.SaveChangesAsync(cancellationToken);
                operationResult.StatusCode = Statuses.Success;
            }
            catch (Exception e)
            {
                operationResult.StatusCode = Statuses.Exception;
                operationResult.Message = $"There is {Statuses.Exception.ToString()} in Database, please recheck";
                operationResult.ExceptionDetails = e.StackTrace;
            }

            return operationResult;
        }

        public async Task SoftDelete(T entity)
        {
            entity.DeletedAt = DateTime.Now;
            await _appDBContext.SaveChangesAsync();
        }
    } 
}
    
     

