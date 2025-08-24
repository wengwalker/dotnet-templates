using Api.Domain.Entities;
using Api.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Data.Contexts;

public class ItemsDbContext : DbContext
{
    protected ItemsDbContext() { }

    public ItemsDbContext(DbContextOptions<ItemsDbContext> options) : base(options) { }

    public virtual DbSet<ItemEntity> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
