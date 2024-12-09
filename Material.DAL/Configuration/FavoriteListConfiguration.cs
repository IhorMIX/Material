using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class FavoriteListConfiguration : IEntityTypeConfiguration<FavoriteList>
{
    public void Configure(EntityTypeBuilder<FavoriteList> builder)
    {
        builder.HasMany(f => f.FavoriteMaterials)
            .WithOne(fm => fm.FavoriteList)
            .HasForeignKey(fm => fm.FavoriteListId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(f => f.User)
            .WithOne(u => u.FavoriteList)
            .HasForeignKey<FavoriteList>(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

