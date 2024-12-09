using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class FavoriteMaterialConfiguration : IEntityTypeConfiguration<FavoriteMaterial>
{
    public void Configure(EntityTypeBuilder<FavoriteMaterial> builder)
    {
        builder.HasKey(fm => new { fm.FavoriteListId, fm.MaterialId });
        
        builder.HasOne(fm => fm.FavoriteList)
            .WithMany(f => f.FavoriteMaterials)
            .HasForeignKey(fm => fm.FavoriteListId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        builder.HasOne(fm => fm.Material)
            .WithMany(m => m.FavoriteMaterials)
            .HasForeignKey(fm => fm.MaterialId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



