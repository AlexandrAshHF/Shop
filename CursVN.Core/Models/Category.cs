namespace CursVN.Core.Models
{
    public class Category
    {
        private Category(Guid id, string name, List<Guid> types)
        {
            Id = id;
            Name = name;
            TypesId = types;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> TypesId { get; set; }
        public static ModelWrapper<Category> Create(Guid id, string name, List<Guid> types)
        {
            return new ModelWrapper<Category>(
                    model: new Category(id, name, types),
                    error: string.Empty,
                    valid: true
                );
        }
    }
}
