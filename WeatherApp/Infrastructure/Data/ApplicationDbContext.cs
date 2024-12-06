using Microsoft.EntityFrameworkCore;
using Domain.Common.EntitiesAbstractions;
using Domain.WeatherAggregate;
using Domain.UserAggregate;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Converters;

namespace Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<WeatherRequest> Requests { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore(typeof(DomainEvent));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyValueConverters(typeof(BaseEntityId<int>), typeof(IntToBaseEntityIdConverter<>), x => x.HasIdentityOptions(1, 1));
    }
}