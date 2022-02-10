using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using SkiaSharp;
using Microsoft.Extensions.Logging;

namespace ARINLAB.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ILogger<ImageService> _logger;
        public ImageService(IWebHostEnvironment appEnvironment, ILogger<ImageService> logger)
        {
            _appEnvironment = appEnvironment;
            _logger = logger;
        }
        public bool DeleteImage(string pictureName)
        {
            string path = _appEnvironment.WebRootPath + "/"+pictureName;

            if (!File.Exists(path)) return false;

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public  async Task<string> UploadImage(IFormFile formFile, string path1)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);
            string path = _appEnvironment.WebRootPath + "/Sounds/" + path1+"/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fileStream = new FileStream( path + fileName, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return $"Sounds/{path1}/{fileName}";
        }

        public bool _DeleteImage(string pictureName, string path)
        {
            path = _appEnvironment.WebRootPath + "/images/" + path + "/" + pictureName;

            if (!File.Exists(path)) return false;

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<string> _UploadImage(IFormFile formFile, string path)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);
            path = _appEnvironment.WebRootPath + "/images/" + path + "/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fileStream = new FileStream(path + fileName, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return fileName;
        }
        private int GetLen(string text)
        {
            int l = text.Length;
            if (l > 0 && l < 6)
                return 180;
            if (l >= 6 && l < 10)
                return 130;
            if (l >= 10 && l < 13)
                return 100;
            if (l >= 13 && l < 20)
                return 90;
            return 70;
        }

        private int GetPos(string text)
        {
            int l = text.Length;
            if (l > 0 && l < 6)
                return 400;
            if (l >= 6 && l < 10)
                return 300;
            if (l >= 10 && l < 13)
                return 200;
            if (l >= 13 && l < 20)
                return 150;
            return 100;
        }
        public string CreateImageForExport(string first, string second)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(".bmp");
                string imageFile = _appEnvironment.WebRootPath + "/images/export.bmp";
                _logger.LogInformation(imageFile);
                var resizeFactor = 1f;
                var bitmap = SKBitmap.Decode(imageFile);
                var toBitmap = new SKBitmap((int)Math.Round(bitmap.Width * resizeFactor), (int)Math.Round(bitmap.Height * resizeFactor), bitmap.ColorType, bitmap.AlphaType);

                var canvas = new SKCanvas(toBitmap);
                // Draw a bitmap rescaled
                canvas.SetMatrix(SKMatrix.CreateScale(resizeFactor, resizeFactor));
                canvas.DrawBitmap(bitmap, 0, 0);
                canvas.ResetMatrix();

                var font = SKTypeface.FromFamilyName("Arial");
                var brush1 = new SKPaint
                {
                    Typeface = font,
                    TextSize = GetLen(first),
                    IsAntialias = true,
                    Color = new SKColor(255, 0, 0, 255)

                };
                var brush2 = new SKPaint
                {
                    Typeface = font,
                    TextSize = GetLen(second),
                    IsAntialias = true,
                    Color = new SKColor(6, 108, 30, 255),

                };
                canvas.DrawText(first, GetPos(first), 1200, brush1);
                canvas.DrawText(second, 200, 1530, brush2);

                canvas.Flush();

                var image = SKImage.FromBitmap(toBitmap);

                var data = image.Encode();
                _logger.LogInformation("Saving image");
                using (var stream = new FileStream(_appEnvironment.WebRootPath + "/images/Exported/" + fileName, FileMode.Create, FileAccess.Write))
                    data.SaveTo(stream);

                data.Dispose();
                image.Dispose();
                canvas.Dispose();
                brush1.Dispose();
                brush2.Dispose();
                font.Dispose();
                toBitmap.Dispose();
                bitmap.Dispose();
                return "/images/Exported/" + fileName;

            }catch(Exception e)
            {
                _logger.LogError($"{e.Message}, {e.InnerException}, {e.Source}");
                return "";
            }
        }
    }
}
