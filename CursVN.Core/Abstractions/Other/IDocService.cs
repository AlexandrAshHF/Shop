namespace CursVN.Core.Abstractions.Other
{
    public interface IDocService<TModel> 
    {
        Task<MemoryStream>CreateDocument(TModel model);
    }
}
