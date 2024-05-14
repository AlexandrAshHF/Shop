namespace CursVN.Persistance.Entities
{
    public class ParamValues
    {
        public Guid Id { get; set; }
        public List<string> Values { get; set; }
        public Guid ProductEntityId { get; set; }
    }
}
