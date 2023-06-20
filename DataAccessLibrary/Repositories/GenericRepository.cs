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

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public async Task AddAsync(T obj, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(obj);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(T obj)
        {
            _context.Set<T>().Update(obj);
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}

