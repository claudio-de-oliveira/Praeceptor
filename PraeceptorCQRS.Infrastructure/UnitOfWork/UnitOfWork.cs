// using PraeceptorCQRS.Application.Abstractions;
// using PraeceptorCQRS.Infrastructure.Data;
// 
// namespace PraeceptorCQRS.Infrastructure.UnitOfWork;
// 
// internal sealed class UnitOfWork : IUnitOfWork
// {
//     private readonly PraeceptorCQRSDbContext _dbContext;
// 
//     public UnitOfWork(PraeceptorCQRSDbContext dbContext) => _dbContext = dbContext;
// 
//     public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
//         _dbContext.SaveChangesAsync(cancellationToken);
// }
