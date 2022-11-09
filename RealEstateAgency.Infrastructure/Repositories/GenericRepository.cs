using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var toDelete = await _dbSet.FindAsync(id);

                _dbSet.Remove(toDelete);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
