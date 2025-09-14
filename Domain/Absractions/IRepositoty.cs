using Domain.CoreServices;
using Domain.Entities;

namespace Domain.Absractions
{
    public interface IRepositoty<TEntity> where TEntity : class
    {
        Task<OperationResult<List<TEntity>>> GetAll(CancellationToken cancellationToken);
        Task<OperationResult<TEntity?>> GetById(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<Activity>> GetByIdTest(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<Guid>> Add(TEntity entity, CancellationToken cancellationToken);
        Task<OperationResult<Guid>> Edit(TEntity entity, CancellationToken cancellationToken);
        Task<OperationResult<TEntity>>Delete(Guid id, CancellationToken cancellationToken);
    }   
   
}
