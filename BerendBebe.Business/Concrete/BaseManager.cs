using BerendBebe.Business.Abstract;
using BerendBebe.DataAccess.Absctract;
using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Concrete
{
    public class BaseManager<TEntity> : IBaseService<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IBaseDal<TEntity> _baseDal;

        public BaseManager(IBaseDal<TEntity> baseDal)
        {
            _baseDal = baseDal;
        }

        public TEntity Add(TEntity entity)
        {
            _baseDal.Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _baseDal.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _baseDal.FindByIdAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _baseDal.GetAllAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await _baseDal.DeleteAsync(entity);
        }

        public async Task RemoveAsync(List<TEntity> entities)
        {
            await _baseDal.DeleteAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _baseDal.UpdateAsync(entity);
        }
    }
}
