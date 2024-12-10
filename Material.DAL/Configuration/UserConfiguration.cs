using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.HasOne(u => u.FavoriteList)
            .WithOne(fl => fl.User)
            .HasForeignKey<FavoriteList>(fl => fl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}



