namespace CursVN.Persistance.Entities
{
    public class ParameterEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TypeEntity> Types { get; set; }
    }
}
