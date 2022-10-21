using ArdenHotel.Areas.SysAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArdenHotel.Controllers
{
    public class GalleryController : Controller
    {
        private IMediaService mediaService;

        public GalleryController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [Route("gallery")]
        public IActionResult Index()
        {
            var list = mediaService.GetAllMedia().Where(x => x.RoomId == 0).ToList();
            return View(list);
        }
    }
}
