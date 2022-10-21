using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArdenHotel.Areas.SysAdmin.Controllers
{
    [Area("SysAdmin")]
    [Route("adm-home")]
    [Authorize]
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
