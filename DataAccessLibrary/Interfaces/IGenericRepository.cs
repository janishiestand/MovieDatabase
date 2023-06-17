﻿using System;
namespace DataAccessLibrary.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		IQueryable<T> GetAll(CancellationToken cancellationToken);
		Task<T> GetById(int id);
		Task AddAsync(T obj, CancellationToken cancellationToken);
		Task Update(T obj);
        Task Delete(T obj);
        Task SaveChangesAsync(CancellationToken cancellationToken);
	}
}
