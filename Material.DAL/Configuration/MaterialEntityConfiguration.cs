using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class MaterialEntityConfiguration : IEntityTypeConfiguration<MaterialEntity>
{
    public void Configure(EntityTypeBuilder<MaterialEntity> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.HasMany(m => m.FavoriteLists)
            .WithMany(fl => fl.Materials)
            .UsingEntity<Dictionary<string, object>>(
                "FavoriteListMaterial",
                j => j.HasOne<FavoriteList>().WithMany().HasForeignKey("FavoriteListId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<MaterialEntity>().WithMany().HasForeignKey("MaterialId").OnDelete(DeleteBehavior.Cascade)
            );
    }
}


