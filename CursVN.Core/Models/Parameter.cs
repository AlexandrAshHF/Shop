namespace CursVN.Core.Models
{
    public class Parameter
    {
        private Parameter(Guid id, string name, List<string>? allowValues, List<Guid> typesId)
        {
            Id = id;
            Name = name;
            AllowValues = allowValues;
            TypesId = typesId;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string>? AllowValues { get; set; }
        public List<Guid> TypesId { get; set; }
        public static ModelWrapper<Parameter> Create(Guid id, string name, List<string>?allowValues, List<Guid> typesId)
        {
            return new ModelWrapper<Parameter>(
                    model: new Parameter(id, name, allowValues, typesId),
                    error: string.Empty,
                    valid: true
                );
        }
    }
}