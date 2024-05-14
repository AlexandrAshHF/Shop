namespace CursVN.Persistance.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TypeEntity> Types { get; set; }
        public string ImageLink { get; set; }
    }
}
