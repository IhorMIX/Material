using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.AuthorizationInfo)
            .WithOne()
            .HasForeignKey<AuthorizationInfo>(a => a.UserId);

        builder.HasMany(u => u.FavoriteListMaterials)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);
    }
}



