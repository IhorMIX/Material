using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.HasOne(i => i.AuthorizationInfo)
            .WithOne(i => i.User)
            .HasForeignKey<AuthorizationInfo>(i => i.UserId);

        builder.HasMany(i => i.Materials)
            .WithOne()
            .HasForeignKey(f => f.UserId);
    }
}