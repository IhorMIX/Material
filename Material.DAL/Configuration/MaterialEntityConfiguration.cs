using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class MaterialEntityConfiguration : IEntityTypeConfiguration<MaterialEntity>
{
    public void Configure(EntityTypeBuilder<MaterialEntity> builder)
    {
        builder.HasOne(m => m.User)
            .WithMany(u => u.Materials)
            .HasForeignKey(m => m.UserId);
        
        builder.HasMany(m => m.FavoriteMaterials)
            .WithOne(fm => fm.Material)
            .HasForeignKey(fm => fm.MaterialId);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.TestNameParam)
            .HasMaxLength(100);
    }
}
