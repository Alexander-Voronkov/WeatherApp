using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using Infrastructure.Data.Converters;

namespace Infrastructure.Data;
internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var connectionString = "Host=localhost;Port=5432;Database=WeatherAppDb;Username=admin;Password=adminpass";

        optionsBuilder.UseNpgsql(connectionString);
        
        optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
