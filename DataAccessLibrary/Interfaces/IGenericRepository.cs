using System;

namespace DataAccessLibrary.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
        public Task AddAsync(T obj, CancellationToken cancellationToken);
        public void Update(T obj);
        public void Delete(T obj);
        public Task SaveChangesAsync(CancellationToken cancellationToken);
        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<T?> FindAsync(int id, CancellationToken cancellationToken);

    }
}

