using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Material.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id); // Основний ключ

        builder.HasOne(u => u.AuthorizationInfo) // Відносини з AuthorizationInfo
            .WithOne() // Один до одного
            .HasForeignKey<AuthorizationInfo>(a => a.UserId); // Зовнішній ключ в AuthorizationInfo

        builder.HasMany(u => u.FavoriteListMaterials) // Відносини з FavoriteListMaterials
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);
    }
}



