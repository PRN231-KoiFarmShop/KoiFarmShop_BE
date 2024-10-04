using ks.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ks.infras.FluentApis;
public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderLines)
            .HasForeignKey(x => x.OrderId);
        builder.HasOne(x => x.Fish)
                .WithOne(x => x.OrderLine)
                .IsRequired(false);
        builder.HasOne(x => x.FishPackage)
            .WithOne(x => x.OrderLine)
            .IsRequired(false); 
    }
}