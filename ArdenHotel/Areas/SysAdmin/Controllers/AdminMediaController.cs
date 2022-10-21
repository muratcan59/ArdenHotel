using ArdenHotel.Areas.SysAdmin.Services;
using ArdenHotel.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArdenHotel.Areas.SysAdmin.Controllers
{
    [Area("SysAdmin")]
    [Authorize]
    public class AdminMediaController : Controller
    {
        private IMediaService mediaService;

        public AdminMediaController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [Route("media")]
        public IActionResult Index()
        {
            var allMedia = mediaService.GetAllMedia();
            return View(allMedia);
        }

        [Route("insert-media")]
        public IActionResult InsertMedia()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertMedia(Media media, IEnumerable<IFormFile> files)
        {
            var urlList = string.Empty;
            var resizedUrlList = string.Empty;
            string path = Path.Combine("wwwroot/Files/");
            foreach (var item in files)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                FileInfo fileInfo = new FileInfo(item.FileName);
                string fileName = item.FileName;
                string fileNameWithPath = Path.Combine(path, fileName);
                string newPath = Path.Combine(path, "new" + fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    item.CopyTo(stream);
                }

                using (var image = Image.Load(item.OpenReadStream()))
                {
                    string newSize = ImageResize(image, 360, 360);
                    string[] sizeArray = newSize.Split(',');
                    image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));                    
                    image.Save(newPath);
                }

                resizedUrlList += "new" + fileName + ",";
                urlList += fileName + ",";
            }
            urlList = urlList.TrimEnd(',');
            resizedUrlList = resizedUrlList.TrimEnd(',');

            media.Url = urlList;
            media.ResizedUrl = resizedUrlList;
            media.RoomId = 0;
            media.CreatedDateUtc = DateTime.UtcNow;
            mediaService.InsertMedia(media);

            return Redirect("~/media");
        }

        [Route("delete-media")]
        public IActionResult DeleteMedia(int id, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = Path.Combine("wwwroot/Files/", url);
                FileInfo fileInfo = new FileInfo(url);
                fileInfo.Delete();
            }
            mediaService.DeleteMedia(id);
            return Redirect("~/media");
        }

        [Route("update-media")]
        public IActionResult UpdateMedia(int id)
        {
            var data = mediaService.GetMediaById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateMedia(Media media, IEnumerable<IFormFile> files)
        {
            var urlList = string.Empty;
            var resizedUrlList = string.Empty;
            string path = Path.Combine("wwwroot/Files/");
            foreach (var item in files)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                FileInfo fileInfo = new FileInfo(item.FileName);
                string fileName = item.FileName;
                string fileNameWithPath = Path.Combine(path, fileName);
                string newPath = Path.Combine(path, "new" + fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    item.CopyTo(stream);
                }

                if (media.Url.Contains(fileName))            
                    continue;

                using (var image = Image.Load(item.OpenReadStream()))
                {
                    string newSize = ImageResize(image, 360, 360);
                    string[] sizeArray = newSize.Split(',');
                    image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                    image.Save(newPath);
                }

                resizedUrlList += "new" + fileName + ",";
                urlList = fileName + ",";
            }

            urlList = urlList.TrimEnd(',');
            media.Url = urlList;
            mediaService.UpdateMedia(media);

            return Redirect("~/media");
        }
        public string ImageResize(Image img, int maxWidth, int maxHeight)
        {
            if (img.Width > maxWidth || img.Height > maxHeight)
            {
                double widthRatio = img.Width / maxWidth;
                double heightRatio = img.Height / maxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }
    }
}
