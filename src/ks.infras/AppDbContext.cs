using ks.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ks.infras;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: AssemblyReference.Assembly);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<User>().HasData();

        modelBuilder.Entity<Fish>().HasData();

        modelBuilder.Entity<Order>().HasData();

        modelBuilder.Entity<OrderLine>().HasData();

        modelBuilder.Entity<Feedback>().HasData();
    }


}