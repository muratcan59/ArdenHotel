using ArdenHotel.Areas.SysAdmin.Services;
using ArdenHotel.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArdenHotel.Controllers
{
    public class RoomController : Controller
    {
        private IRoomService roomService;
        private ICategoryService categoryService;

        public RoomController(IRoomService roomService, ICategoryService categoryService)
        {
            this.roomService = roomService;
            this.categoryService = categoryService;
        }

        [Route("rooms")]
        public IActionResult Index(int catId)
        {
            var list = roomService.GetAllRooms();

            ViewData["category"] = catId == 0 ? "Odalar" : categoryService.GetCategory(catId).CategoryName;

            if (list != null)
            {
                list = catId == 0 ? list.ToList() : list.Where(c => c.CategoryID == catId).ToList();
                return View(list);
            }
            return View();
        }

        [Route("room-detail")]
        public IActionResult RoomDetail(int id)
        {
            var data = roomService.GetRoomComment(id);

            return View(data);
        }
    }
}
