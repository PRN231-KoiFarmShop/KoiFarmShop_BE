using ks.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ks.infras.FluentApis;
public class FishConfiguration : IEntityTypeConfiguration<Fish>
{
    public void Configure(EntityTypeBuilder<Fish> builder)
    {
        builder.HasOne(x => x.FishPackage)
            .WithMany(x => x.Fishes)
            .HasForeignKey(x => x.FishPackageId)
            .IsRequired(false);
        builder.HasOne(x => x.OrderLine)
            .WithOne(x => x.Fish)
            .IsRequired(false);
            
    }
}