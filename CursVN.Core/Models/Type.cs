namespace CursVN.Core.Models
{
    public class Type
    {
        private Type(Guid id, string name, List<Guid> parametersId,
            List<Guid> productsId, Guid categoryId)
        {
            Id = id;
            Name = name;
            ParametersId = parametersId;
            ProductsId = productsId;
            CategoryId = categoryId;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> ParametersId { get; set; }
        public List<Guid> ProductsId { get; set; }
        public Guid CategoryId { get; set; }

        public static ModelWrapper<Type> Create(Guid id, string name, List<Guid> parametersId,
            List<Guid> productsId, Guid categoryId)
        {
            return new ModelWrapper<Type>(
                model: new Type(id, name, parametersId, productsId, categoryId),
                error: string.Empty,
                valid: true
                );
        }
    }
}
