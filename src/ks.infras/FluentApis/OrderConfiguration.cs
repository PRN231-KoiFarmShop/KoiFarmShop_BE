using ks.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ks.infras.FluentApis;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);
        builder.HasMany(x => x.OrderLines)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);
        builder.HasMany(x => x.Feedbacks)
            .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
    }
}