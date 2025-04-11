using Microsoft.EntityFrameworkCore;

namespace Intelsoft.Common.Outbox.EntityFrameworkCore;

/// <summary>
///     Provides extension methods for applying EF Core configurations related to outbox.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies outbox schema configuration.
    /// </summary>
    public static ModelBuilder ApplyOutbox(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        return modelBuilder;
    }
}
