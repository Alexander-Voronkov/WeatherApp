using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.WeatherAggregate;
using Domain.UserAggregate;

namespace Infrastructure.Data.Configurations;
internal class WeatherRequestConfiguration : IEntityTypeConfiguration<WeatherRequest>
{
    public void Configure(EntityTypeBuilder<WeatherRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("WeatherRequests");

        builder.Property<string>("_city")
            .HasColumnName("City");

        builder.Property<DateTimeOffset>("_requestedAt")
            .HasColumnName("RequestedAt");

        builder.Property<string>("_rawJsonResponse")
            .HasColumnName("RawJsonData");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey("_userId");

        builder.Property<UserId>("_userId")
            .HasColumnName("UserId");
    }
}