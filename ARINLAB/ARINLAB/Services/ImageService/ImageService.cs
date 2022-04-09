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
using SkiaSharp.HarfBuzz;

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
        private static int GetPos1(int length, int chl)
        {
            const int center = 520;
            return center - chl * (length / 2);
        }

        private static int GetPos2(int length, int chl)
        {
            const int center = 520;

            return center - chl * (length / 2) - chl / 2;
        }
        private static int TmPos(int length, int chl)
        {
            const int center = 520;
            return center - chl * (length / 2);
        }

        private static int ArabPos(int length, int chl)
        {
            const int center = 520;

            return center - chl * (length / 2) - chl / 2;
        }
        public string CreateImageForExport(string first, string second)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(".jpg");
                string imageFile = _appEnvironment.WebRootPath + "/images/export.jpg";
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
                var fontArab = SKTypeface.FromFamilyName("Arial");
                var brush1 = new SKPaint
                {
                    Typeface = fontArab,
                    TextSize = 100,
                    IsAntialias = true,
                    Color = new SKColor(0, 0, 0, 255)

                };
                var brush2 = new SKPaint
                {
                    Typeface = font,
                    TextSize = 100,
                    IsAntialias = true,
                    Color = new SKColor(0, 0, 0, 255),

                };
                int pos1 = GetPos1(first.Length, 55);
                int pos2 = GetPos1(second.Length, 55);
                // canvas.DrawText(first, pos1, 1100, brush1);
                 canvas.DrawText(second, pos2, 1500, brush2);
                using (var tf = SKFontManager.Default.MatchCharacter('ئ'))
                using (var shaper = new SKShaper(tf))
                using (var paint = new SKPaint { TextSize = 100, Typeface = tf })
                {                    
                    canvas.DrawShapedText(shaper, first, pos1, 1100, paint);
                }
                canvas.DrawText(second, pos2, 1500, brush2);
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
        public string PhraseExport(string arabPh, string readsTm, string turkmenPh, string readsAr)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(".jpg");
                string imageFile = _appEnvironment.WebRootPath + "/images/export.jpg";

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
                    TextSize = (turkmenPh.Length > 35)? 45:55,
                    IsAntialias = true,                 
                    Color = new SKColor(0, 0, 0, 255)

                };
                var brush2 = new SKPaint
                {
                    Typeface = font,
                    TextSize = (readsTm.Length > 35)? 45:55,
                    IsAntialias = true,
                    Color = new SKColor(53, 32, 135, 255),

                };
                int h1 = 1055;
                int h2 = 1135;
                int h3 = 1435;
                int h4 = 1535;
                int pos1 = ArabPos(arabPh.Length, (arabPh.Length > 35) ? 20 : 25);
                int pos2 = TmPos(readsTm.Length, (readsTm.Length > 35) ? 20 : 25);
                int pos3 = TmPos(turkmenPh.Length, (turkmenPh.Length > 35) ? 20 : 25);
                int pos4 = ArabPos(readsAr.Length, (readsAr.Length > 35) ? 20 : 25);

                //canvas.DrawText(arabPh, pos1, h1, brush1);
                using (var tf = SKFontManager.Default.MatchCharacter('ئ'))
                using (var shaper = new SKShaper(tf))
                using (var paint = new SKPaint { TextSize = arabPh.Length > 35 ? 45 : 55, Typeface = tf })
                {
                    canvas.DrawShapedText(shaper, arabPh, pos1, h1, paint);
                }
                
                canvas.DrawText(readsTm, pos2, h2, brush2);
                canvas.DrawText(turkmenPh, pos3, h3, brush1);
                //canvas.DrawText(readsAr, pos4, h4, brush2);
                using (var tf = SKFontManager.Default.MatchCharacter('ئ'))
                using (var shaper = new SKShaper(tf))
                using (var paint = new SKPaint { TextSize = readsAr.Length>35?45:55, Typeface = tf })
                {
                    canvas.DrawShapedText(shaper, readsAr, pos4, h4, paint);
                }
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
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
