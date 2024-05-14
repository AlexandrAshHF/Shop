namespace CursVN.Core.Models
{
    public class Category
    {
        private Category(Guid id, string name, List<Guid> types, string imgLink)
        {
            Id = id;
            Name = name;
            TypesId = types;
            ImageLink = imgLink;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public List<Guid> TypesId { get; set; }
        public static ModelWrapper<Category> Create(Guid id, string name, List<Guid> types, string imgLink)
        {
            return new ModelWrapper<Category>(
                    model: new Category(id, name, types, imgLink),
                    error: string.Empty,
                    valid: true
                );
        }
    }
}
