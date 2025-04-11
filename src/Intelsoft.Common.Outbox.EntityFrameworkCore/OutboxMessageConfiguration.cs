using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intelsoft.Common.Outbox.EntityFrameworkCore;

/// <summary>
///     Explicitly configures the schema for the <see cref="OutboxMessage"/> entity.
/// </summary>
public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(512)
            .HasColumnName("type");

        builder.Property(x => x.Payload)
            .IsRequired()
            .HasColumnName("payload");

        builder.Property(x => x.Topic)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("topic");

        builder.Property(x => x.OccurredOn)
            .IsRequired()
            .HasColumnName("occurred_on");

        builder.Property(x => x.CorrelationId)
            .HasMaxLength(128)
            .HasColumnName("correlation_id");

        builder.Property(x => x.ProcessedAt)
            .HasColumnName("processed_at");

        builder.HasIndex(x => x.ProcessedAt)
            .HasDatabaseName("ix_outbox_messages_processed_at");
    }
}
