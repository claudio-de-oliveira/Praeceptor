using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

using PraeceptorCQRS.Application.Abstractions.Messaging;
using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Infrastructure.Outbox;

namespace PraeceptorCQRS.Infrastructure.Idempotence
{
    public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly INotificationHandler<TDomainEvent> _decorated;
        private readonly PraeceptorCQRSDbContext _dbContext;

        public IdempotentDomainEventHandler(
            INotificationHandler<TDomainEvent> decorated,
            PraeceptorCQRSDbContext dbContext)
        {
            _decorated = decorated;
            _dbContext = dbContext;
        }

        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            string consumer = _decorated.GetType().Name;
            
            var table = _dbContext.Set<OutboxMessageConsumer>();
            Guard.Against.Null(table);
            
            if (await _dbContext.Set<OutboxMessageConsumer>()
                    .AnyAsync(
                        outboxMessageConsumer =>
                            outboxMessageConsumer.Id == notification.Id &&
                            outboxMessageConsumer.Name == consumer,
                        cancellationToken))
            {
                return;
            }
            
            await _decorated.Handle(notification, cancellationToken);
            
            _dbContext.Set<OutboxMessageConsumer>()
                .Add(new OutboxMessageConsumer
                {
                    Id = notification.Id,
                    Name = consumer
                });
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
