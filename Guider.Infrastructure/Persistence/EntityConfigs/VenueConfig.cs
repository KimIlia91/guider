using Guider.Domain.Categories;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guider.Infrastructure.Persistence.EntityConfigs;

internal sealed class VenueConfig : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("venues");
        
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => VenueId.Convert(value));

        builder
            .Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(VenueConstants.NameMaxLength);

        builder
            .Property(e => e.Description)
            .HasColumnName("description")
            .IsRequired(false)
            .HasMaxLength(VenueConstants.DescriptionMaxLenght);

        builder
            .Property(e => e.Address)
            .HasColumnName("address")
            .IsRequired()
            .HasMaxLength(VenueConstants.AddressMaxLenght);

        builder
            .Property(e => e.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired()
            .HasDefaultValue(false);

        builder
            .Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder
            .Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder
            .HasMany(e => e.Tags)
            .WithMany(e => e.Venues)
            .UsingEntity(e => e.ToTable("venues_tags"));

        builder
            .HasOne<Category>()
            .WithMany(e => e.Venues)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}