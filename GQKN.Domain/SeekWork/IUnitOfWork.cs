﻿namespace PVI.GQKN.Domain.Seedwork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
}