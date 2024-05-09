namespace CursVN.Persistance.Entities
{
    public class TypeEntity
    {
        public Guid Id { get; set; }
        public Guid ParrentId { get; set; }
        public string Name { get; set; }
        public List<ParameterEntity> Parameters { get; set; }
        public List<ProductEntity> Products { get; set; }
        public CategoryEntity Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
