using ks.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ks.infras.FluentApis;
public class FishPackageConfiguration : IEntityTypeConfiguration<FishPackage>
{
    public void Configure(EntityTypeBuilder<FishPackage> builder)
    {
        builder.HasMany(x => x.Fishes)
            .WithOne(x => x.FishPackage)
            .HasForeignKey(x => x.FishPackageId)
            .IsRequired(false);
    }
}
