using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ARINLAB.Services.ImageService
{
    public interface IImageService
    {
        public Task<string> UploadImage(IFormFile formFile, string pClass);
        public bool DeleteImage(string pictureName);

        public Task<string> _UploadImage(IFormFile formFile, string pClass);
        public bool _DeleteImage(string pictureName, string pClass);

        public string CreateImageForExport(string first, string second);
    }
}
