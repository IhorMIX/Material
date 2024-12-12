using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class MaterialEntityConfiguration : IEntityTypeConfiguration<MaterialEntity>
{
    public void Configure(EntityTypeBuilder<MaterialEntity> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.HasKey(m => m.Id); // Основний ключ

        builder.HasMany(m => m.FavoriteListMaterials) // Відносини з FavoriteListMaterials
            .WithOne(f => f.Material)
            .HasForeignKey(f => f.MaterialId);
    }
}

