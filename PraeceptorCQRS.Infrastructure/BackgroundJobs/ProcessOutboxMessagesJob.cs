using MediatR;
using Newtonsoft.Json;
using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Quartz;
using PraeceptorCQRS.Infrastructure.Outbox;

namespace PraeceptorCQRS.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly PraeceptorCQRSDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(PraeceptorCQRSDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    // Notificação de modificações no banco de dados de escrita
    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null && m.Error == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    JsonSerializerSettings);

            if (domainEvent is null)
                continue;

            AsyncRetryPolicy policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromMilliseconds(50 * attempt));

            PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>
                _publisher.Publish(
                    domainEvent,
                    context.CancellationToken));

            outboxMessage.Error = result.FinalException?.ToString();
            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
