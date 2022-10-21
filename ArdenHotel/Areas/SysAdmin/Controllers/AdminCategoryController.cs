using ArdenHotel.Areas.SysAdmin.Services;
using ArdenHotel.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArdenHotel.Areas.SysAdmin.Controllers
{
    [Area("SysAdmin")]
    [Authorize]
    public class AdminCategoryController : Controller
    {
        private ICategoryService categoryService;

        public AdminCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [Route("category")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("update-category")]
        public IActionResult UpdateCategory(int id)
        {
            var data = categoryService.GetCategory(id);
            if (data != null)
            {
                return View(data);
            }

            return Redirect("~/category-list");
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (category != null)
            {
                categoryService.UpdateCategory(category);
                return Redirect("~/category-list");
            }
            return View();
        }


        [HttpGet]
        [Route("insert-category")]
        public IActionResult InsertCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertCategory(Category category)
        {
            if (category != null)
            {
                categoryService.InsertCategory(category);
                return Redirect("~/category");
            }
            return View();
        }

        [Route("category-list")]
        public IActionResult GetAllCategory()
        {
            var data = categoryService.ListCategory();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return View();
            }
        }

        [Route("delete-category")]
        public IActionResult DeleteCategory(int id)
        {
            if (id != 0)
            {
                categoryService.DeleteCategory(id);
                return Redirect("~/category-list");
            }
            return Redirect("~/category-list"); ;
        }
    }
}
