using ArdenHotel.Areas.SysAdmin.Services;
using ArdenHotel.Models;
using ArdenHotel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace ArdenHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHtmlLocalizer<SharedResource> htmlLocalizer;
        private ICommentService commentService;
        private ICategoryService categoryService;
        public HomeController(ILogger<HomeController> logger, IHtmlLocalizer<SharedResource> htmlLocalizer, ICommentService commentService, ICategoryService categoryService)
        {
            _logger = logger;
            this.htmlLocalizer = htmlLocalizer;
            this.commentService = commentService;
            this.categoryService = categoryService;
        }

        [Route("")]
        public IActionResult Index()
        {
            ViewData["items"] = categoryService.ListCategory();
            return View();
        }

        [Route("about-us")]
        public IActionResult About()
        {
            return View();
        }

        [Route("activities")]
        public IActionResult Activities()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
            );
            return RedirectToAction(nameof(Index));
        }
    }
}
