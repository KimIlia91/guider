using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guider.Infrastructure.Persistence.EntityConfigs;

internal sealed class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CategoryId.Convert(value));

        builder
            .Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(CategoryConstants.NameMaxLength);

        builder
            .Property(e => e.Description)
            .HasColumnName("description")
            .IsRequired(required: false)
            .HasMaxLength(CategoryConstants.DescriptionMaxLenght);

        builder
            .HasMany(e => e.Venues)
            .WithOne()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

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
    }
}