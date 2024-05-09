namespace CursVN.Core.Abstractions.DataServices
{
    public interface ITypeService
    {
        List<Models.Type> GetAll();
        Task<Models.Type> GetById(Guid Id);
        List<Models.Type> GetByParrentId(Guid id);
        List<Models.Type> GetByCategoryId(Guid id);
        Task<Guid> Create(Models.Type type);
        Task<Guid> Update(Models.Type type);
        Task Delete(Guid id);
    }
}
