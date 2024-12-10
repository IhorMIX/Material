using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class FavoriteListConfiguration : IEntityTypeConfiguration<FavoriteList>
{
    public void Configure(EntityTypeBuilder<FavoriteList> builder)
    {
        builder.HasKey(fl => fl.Id);
        
        builder.HasOne(fl => fl.User)
            .WithOne(u => u.FavoriteList)
            .HasForeignKey<FavoriteList>(fl => fl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(fl => fl.Materials)
            .WithMany(m => m.FavoriteLists)
            .UsingEntity<Dictionary<string, object>>(
                j => j.HasOne<MaterialEntity>().WithMany().HasForeignKey("MaterialId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<FavoriteList>().WithMany().HasForeignKey("FavoriteListId").OnDelete(DeleteBehavior.Cascade)
            );
    }
}




