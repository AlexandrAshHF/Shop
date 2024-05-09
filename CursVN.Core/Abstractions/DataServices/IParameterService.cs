using CursVN.Core.Models;

namespace CursVN.Core.Abstractions.DataServices
{
    public interface IParameterService
    {
        List<Parameter> GetAll();
        Task<Parameter> GetById(Guid id);
        Task<Guid> Create(Parameter parameter);
        Task<Guid> Update(Parameter parameter);
        Task<Guid> Delete(Guid id);
    }
}
