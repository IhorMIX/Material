using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class FavoriteListMaterialConfiguration : IEntityTypeConfiguration<FavoriteListMaterial>
{
    public void Configure(EntityTypeBuilder<FavoriteListMaterial> builder)
    {
        builder.HasKey(f => f.Id);
        
        builder.HasOne(f => f.User)
            .WithMany(u => u.FavoriteListMaterials)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.Material)
            .WithMany(m => m.FavoriteListMaterials)
            .HasForeignKey(f => f.MaterialId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}




