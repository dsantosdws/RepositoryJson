using RepositoryJson.BaseRepository.BaseEntitys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryJson.BaseRepository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task Save(TEntity model);

        Task Delete(TEntity model);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetBy(Guid Id);

        Task<List<TEntity>> Search(Func<TEntity, bool> predicate);

        Task<TEntity> SearchFirstOrDefault(Func<TEntity, bool> predicate);
    }
}