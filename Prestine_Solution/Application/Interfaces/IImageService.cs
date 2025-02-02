using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImageService
    {
        Task<string> ConvertToBase64Async(IFormFile file);
        bool IsValidImage(IFormFile file);
    }
}
