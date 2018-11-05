using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cican_Micro.Controllers
{
    public class ImageController : Controller
    {

        private readonly IHostingEnvironment _appEnvironment;

        public ImageController (IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpGet] //Load Image
        public IActionResult Upload_Image()
        {
            return View();
        }

        [HttpPost] //Upload Image
        public async Task<IActionResult> Upload_Image(IFormFile file)
        {
            if (file == null || file.Length == 0) return Content("file not selected");
            string path_Root = _appEnvironment.WebRootPath;
            string path_to_Images = path_Root + "\\upload_images\\" + file.FileName;
            using (var stream = new FileStream(path_to_Images, FileMode.Create))
            {
                await file.CopyToAsync(stream); //copy file
            }
            ViewData["Image"] = Url.Content("~/upload_images/" + file.FileName);
            ViewData["FilePath"] = path_to_Images;
            return View();

        }

    }
}