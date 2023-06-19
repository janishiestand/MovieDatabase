using System;
namespace DataAccessLibrary.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
        public Task AddAsync(T obj, CancellationToken cancellationToken);
        public Task Update(T obj);
        public Task Delete(T obj);
        public Task SaveChangesAsync(CancellationToken cancellationToken);
	}
}

