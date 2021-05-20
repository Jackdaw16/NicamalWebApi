using System.Threading.Tasks;

namespace NicamalWebApi.Services
{
    public interface IImageStorage
    {
        Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType);
        Task DeleteFile(string route, string container);
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
    }
}