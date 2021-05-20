using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace NicamalWebApi.Services
{
    public class ImageStorage: IImageStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public ImageStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
        {
            await DeleteFile(route, container);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task DeleteFile(string route, string container)
        {
            if (route != null)
            {
                var fileName = Path.GetFileName(route);
                string fileDirectory = Path.Combine(_env.WebRootPath, container, fileName);

                if (File.Exists(fileDirectory))
                    File.Delete(fileDirectory);
            }
            
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            var nameFile = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, container);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string route = Path.Combine(folder, nameFile);
            await File.WriteAllBytesAsync(route, content);

            var currentUrl =
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            var dbUrl = Path.Combine(currentUrl, container, nameFile).Replace("\\", "/");

            return dbUrl;
        }
    }
}