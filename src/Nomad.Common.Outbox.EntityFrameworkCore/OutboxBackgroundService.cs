using Nomad.Common.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Nomad.Common.Outbox.EntityFrameworkCore;

/// <summary>
///     Background service that periodically publishes unprocessed outbox events.
/// </summary>
public sealed class OutboxBackgroundService(IServiceProvider services, ILogger<OutboxBackgroundService> logger)
    : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Outbox background publisher started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = services.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<IOutboxPublisher>();
                await publisher.PublishPendingAsync(stoppingToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish outbox events.");
            }

            await Task.Delay(_interval, stoppingToken).ConfigureAwait(false);
        }

        logger.LogInformation("Outbox background publisher stopped.");
    }
}
