using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Tags;
using Guider.Domain.Tags.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guider.Infrastructure.Persistence.EntityConfigs;

internal sealed class TagConfig : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");
        
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TagId.Convert(value));

        builder
            .Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(TagConstants.NameLength);

        builder
            .Property(e => e.Description)
            .HasColumnName("description")
            .IsRequired(required: false)
            .HasMaxLength(TagConstants.DescriptionLenght);

        builder
            .Property(e => e.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.HasQueryFilter(e => !e.IsDeleted);
        
        builder.HasIndex(e => e.IsDeleted).HasFilter("is_deleted = 0");

        builder
            .Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder
            .Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder
            .HasMany(e => e.Venues)
            .WithMany(e => e.Tags)
            .UsingEntity(e => e.ToTable("venues_tags"));
        
    }
}