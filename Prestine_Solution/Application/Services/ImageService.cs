using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB

        public async Task<string> ConvertToBase64Async(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file was provided");

            if (!IsValidImage(file))
                throw new ArgumentException("Invalid image file");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            return $"data:{file.ContentType};base64,{Convert.ToBase64String(bytes)}";
        }

        public bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > _maxFileSize)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }
    }
}
