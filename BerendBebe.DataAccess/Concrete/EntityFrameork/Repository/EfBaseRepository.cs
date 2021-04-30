using BerendBebe.Entities.Abstract;
using BerendBebe.DataAccess.Absctract;
using BerendBebe.DataAccess.Concrete.EntityFrameork.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DataAccess.Concrete.EntityFrameork.Repository
{
    public class EfBaseRepository<TEntity> : IBaseDal<TEntity>
        where TEntity : class, IEntity, new()
    {
        public TEntity Add(TEntity entity)
        {
            using var context = new BerendBebeContext();
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using var context = new BerendBebeContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using var context = new BerendBebeContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(List<TEntity> entities)
        {
            using BerendBebeContext context = new BerendBebeContext();
            context.RemoveRange(entities);
            await context.SaveChangesAsync();

        }


        public async Task<TEntity> FindByIdAsync(int id)
        {
            using var context = new BerendBebeContext();
            return await context.FindAsync<TEntity>(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            using var context = new BerendBebeContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new BerendBebeContext();
            return await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> keySelector)
        {
            using var context = new BerendBebeContext();
            return await context.Set<TEntity>().Where(filter).OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync<Tkey>(Expression<Func<TEntity, Tkey>> keySelector)
        {
            using var context = new BerendBebeContext();
            return await context.Set<TEntity>().OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new BerendBebeContext();
            return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var context = new BerendBebeContext();
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
