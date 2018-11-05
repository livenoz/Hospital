using System;
using System.Collections.Generic;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HMS.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly HMSContext _context;

        public IQueryable<T> Repo => _context.Set<T>();

        public Repository(HMSContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public void Delete(int id)
        {
            var model = _context.Set<T>().Find(id);
            if (model != null)
            {
                _context.Set<T>().Remove(model);
            }
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            var models = _context.Set<T>().ToList();
            return models;
        }

        T IRepository<T>.Get(int id)
        {
            var model = _context.Set<T>().Find(id);
            return model;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var models = await _context.Set<T>().ToListAsync();
            return models;
        }

        public async Task<T> GetAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            return model;
        }

        public int SaveChange()
        {
            int result = 0;
            try
            {
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: write log here
            }
            return result;
        }

        public async Task<int> SaveChangeAsync()
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

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            if (model != null)
            {
                _context.Set<T>().Remove(model);
            }
        }
    }
}
