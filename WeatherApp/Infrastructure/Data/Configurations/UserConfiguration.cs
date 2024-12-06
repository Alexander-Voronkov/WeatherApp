using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Infrastructure.Data.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.ToTable("Users");

        builder.ComplexProperty<UserPassword>("_password", navigationBuilder =>
        {
            navigationBuilder.Property(p => p.Value)
                .HasMaxLength(256)
                .IsRequired()
                .HasColumnName("PasswordHash");
        });

        builder.ComplexProperty<UserEmail>("_email", navigationBuilder =>
        {
            navigationBuilder.Property(e => e.Value)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("Email");
        });

        builder.Property<DateTimeOffset>("_registeredAt")
            .HasColumnName("RegisteredAt");
    }
}