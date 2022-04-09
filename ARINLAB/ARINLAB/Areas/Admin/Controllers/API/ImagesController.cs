using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;
        public ImagesController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            string fileName = "";
            string folderName = "/Files/Content/UploadService/";
            IFormFile file;
            try
            {
                file = Request.Form.Files[0];
                string newPath = _appEnvironment.WebRootPath + folderName;

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(newPath + fileName, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                return Ok(new { location = $"{folderName}{fileName}" });
            }
            catch (System.Exception ex)
            {
                return Ok("Upload Failed: " + ex.Message);
            }

            //string imageUrl = "https://dustyhoppe-blog-images-prod.sfo2.cdn.digitaloceanspaces.com/cat-image-qf76g35k.jpg";
            //return Ok(new { imageUrl });

            //return UnprocessableEntity(new { Message = $"Cannot determine content type for file '{model.FileName}'." });
        }
    }
}
