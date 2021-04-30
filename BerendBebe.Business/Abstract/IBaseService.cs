using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface IBaseService<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(int id);
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task RemoveAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
    }
}
