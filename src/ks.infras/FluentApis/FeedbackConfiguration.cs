using ks.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ks.infras.FluentApis;
public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Order)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.OrderId);
    }
}