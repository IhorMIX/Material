using Material.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Material.DAL
{
    public class MaterialDbContext(DbContextOptions<MaterialDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MaterialEntity> Materials { get; set; }
        public DbSet<FavoriteList> FavoriteLists { get; set; }
        public DbSet<AuthorizationInfo> AuthorizationInfos { get; set; }
    }
}

