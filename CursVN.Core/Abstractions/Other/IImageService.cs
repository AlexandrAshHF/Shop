namespace CursVN.Core.Abstractions.Other
{
    public interface IImageService
    {
        Task<string> Upload(MemoryStream ms);
    }
}
