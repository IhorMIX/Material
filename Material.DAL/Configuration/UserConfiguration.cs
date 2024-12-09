using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Login).IsUnique();
        
        builder.HasOne(u => u.FavoriteList)
            .WithOne(f => f.User)
            .HasForeignKey<FavoriteList>(f => f.UserId);
        
        builder.HasOne(u => u.AuthorizationInfo)
            .WithOne(a => a.User)
            .HasForeignKey<AuthorizationInfo>(a => a.UserId);
        
        builder.HasMany(u => u.Materials)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserId);
    }
}

