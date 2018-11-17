using System;
using System.Collections.Generic;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace HMS.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly HealthContext _context;

        public virtual IQueryable<TEntity> Repo => _context.Set<TEntity>();

        public BaseRepository(HealthContext context)
        {
            _context = context;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity).Entity;
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            var models = await _context.Set<TEntity>().ToListAsync();
            return models;
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            var models = await _context.Set<TEntity>().Where(expression).ToListAsync();
            return models;
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            var model = await _context.Set<TEntity>().FindAsync(id);
            return model;
        }

        public virtual async Task<int> SaveChangeAsync()
        {
            int result = 0;
            try
            {
                result = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODO: write log here
            }
            return result;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var model = await _context.Set<TEntity>().FindAsync(id);
            if (model != null)
            {
                _context.Set<TEntity>().Remove(model);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

    }
}
