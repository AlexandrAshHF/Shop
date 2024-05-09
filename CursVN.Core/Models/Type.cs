namespace CursVN.Core.Models
{
    public class Type
    {
        private Type(Guid id, Guid parrentId, string name, List<Guid> parametersId,
            List<Guid> productsId, Guid categoryId)
        {
            Id = id;
            ParrentId = parrentId;
            Name = name;
            ParametersId = parametersId;
            ProductsId = productsId;
            CategoryId = categoryId;
        }
        public Guid Id { get; set; }
        public Guid ParrentId { get; set; }
        public string Name { get; set; }
        public List<Guid> ParametersId { get; set; }
        public List<Guid> ProductsId { get; set; }
        public Guid CategoryId { get; set; }

        public static ModelWrapper<Type> Create(Guid id, Guid parrentId, string name, List<Guid> parametersId,
            List<Guid> productsId, Guid categoryId)
        {
            return new ModelWrapper<Type>(
                model: new Type(id, parrentId, name, parametersId, productsId, categoryId),
                error: string.Empty,
                valid: true
                );
        }
    }
}
