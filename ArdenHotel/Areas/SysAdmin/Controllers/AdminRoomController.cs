using ArdenHotel.Areas.SysAdmin.Services;
using ArdenHotel.Data;
using ArdenHotel.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace ArdenHotel.Areas.SysAdmin.Controllers
{
    [Area("SysAdmin")]
    [Authorize]
    public class AdminRoomController : Controller
    {
        private IRoomService roomService;
        private ICategoryService categoryService;
        private IMediaService mediaService;
        private ApplicationDbContext context;

        public AdminRoomController(IRoomService roomService, ICategoryService categoryService, IMediaService mediaService, ApplicationDbContext context)
        {
            this.roomService = roomService;
            this.categoryService = categoryService;
            this.mediaService = mediaService;
            this.context = context;
        }

        [Route("room")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("insert-room")]
        public IActionResult InsertRoom()
        {
            ViewData["items"] = categoryService.ListCategory().Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() });
            return View();
        }

        [HttpPost]
        public IActionResult InsertRoom(Room room, IEnumerable<IFormFile> file)
        {
            if (room != null)
            {
                room.CreatedDateUtc = DateTime.UtcNow;
                roomService.InsertRoom(room);
            }

            var urlList = string.Empty;
            var resizedUrlList = string.Empty;
            string path = Path.Combine("wwwroot/Files/");
            foreach (var item in file)
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
            Media media = new Media()
            {
                RoomId = room.RoomID,
                Url = urlList,
                CreatedDateUtc = DateTime.Now
            };
            mediaService.InsertMedia(media);

            return Redirect("~/room");

        }

        [Route("update-room")]
        public IActionResult UpdateRoom(int id)
        {
            if (id != 0)
            {
                ViewData["items"] = categoryService.ListCategory().Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() });

                var data = roomService.GetRoom(id);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateRoom(Room room, IEnumerable<IFormFile> files)
        {
            if (room != null)
            {
                room.ModifiedDateUtc = DateTime.UtcNow;
                roomService.UpdateRoom(room);

                var urlList = string.Empty;
                var resizedUrlList = string.Empty;
                string path = Path.Combine("wwwroot/Files/");

                var mediaData = context.Set<Media>().Where(x => x.RoomId == room.RoomID).FirstOrDefault();
                urlList = mediaData != null ? mediaData.Url + "," : string.Empty;
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

                    if (mediaData != null && mediaData.Url.Contains(fileName))
                        continue;

                    resizedUrlList += "new" + fileName + ",";                
                    urlList += fileName + ",";
                }

                urlList = urlList.TrimEnd(',');
                if (mediaData != null)
                {
                    mediaData.Url = urlList;
                    mediaData.ResizedUrl = resizedUrlList;
                    mediaData.ModifiedDateUtc = DateTime.UtcNow;
                    mediaService.UpdateMedia(mediaData);
                }
                else
                {
                    Media media = new Media()
                    {
                        Url = urlList,
                        ResizedUrl = resizedUrlList,
                        RoomId = room.RoomID,
                        CreatedDateUtc = DateTime.UtcNow,
                    };
                    mediaService.InsertMedia(media);
                }
                return Redirect("~/room-list");
            }

            return View();
        }

        [Route("room-list")]
        public IActionResult GetAllRoom()
        {
            var list = roomService.GetAllRooms();
            if (list != null)
            {
                return View(list);
            }
            return View();
        }

        [Route("delete-room")]
        public IActionResult DeleteRoom(int id)
        {
            if (id != 0)
            {
                var allMedia = mediaService.GetAllMedia();
                foreach (var item in allMedia)
                {
                    var detectEquals = allMedia.Where(x => x.RoomId == id).FirstOrDefault();
                    if (detectEquals != null)
                    {
                        detectEquals.Url = Path.Combine("wwwroot/Files/", detectEquals.Url);
                        FileInfo fileInfo = new FileInfo(detectEquals.Url);
                        if (fileInfo.Exists)
                        {
                            fileInfo.Delete();
                        }

                        mediaService.DeleteMedia(detectEquals.MediaID);
                    }
                }
                roomService.DeleteRoom(id);
                return Redirect("~/room-list");
            }

            return Redirect("~/room-list");
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
