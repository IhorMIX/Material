namespace Material.DAL.Entity
{
    public class FavoriteMaterial : BaseEntity
    {
        public int FavoriteListId { get; set; }
        public FavoriteList FavoriteList { get; set; } = null!;

        public int MaterialId { get; set; }
        public MaterialEntity Material { get; set; } = null!;
    }
}