using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.MongoDbRepo
{
    public interface IMongoDbRepository<T> where T : class, IBaseEntity

    {
        public  Task<List<T>> GetAsync();

        public  Task<T?> GetByIdAsync(string id);

        public  Task CreateAsync(T newEntity);

        public  Task UpdateAsync(string id, T updatedEntity);

        public  Task RemoveAsync(string id);
    }
}
