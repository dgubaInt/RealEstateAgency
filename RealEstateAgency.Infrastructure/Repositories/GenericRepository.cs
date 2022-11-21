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

        public async Task<bool> AddAsync(T entity)
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var toDelete = await _dbSet.FindAsync(id);

                return await DeleteAsync(toDelete); ;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
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
