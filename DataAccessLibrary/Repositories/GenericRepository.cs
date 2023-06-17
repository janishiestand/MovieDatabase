using System;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly MovieContext _context = null;
         

        public GenericRepository(MovieContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(CancellationToken cancellationToken)
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T obj, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(obj);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(T obj)
        {
            _context.Set<T>().Update(obj);
        }
    }
}

