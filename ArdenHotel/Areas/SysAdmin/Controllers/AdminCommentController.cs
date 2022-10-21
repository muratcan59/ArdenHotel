using ArdenHotel.Entities;
using ArdenHotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArdenHotel.Areas.SysAdmin.Controllers
{
    [Area("SysAdmin")]
    [Authorize]
    public class AdminCommentController : Controller
    {
        private ICommentService commentService;

        public AdminCommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [Route("comment")]
        public IActionResult Index()
        {
            var list = commentService.GetListWithRoom();
            return View(list);
        }

        [Route("update-comment")]
        public IActionResult UpdateComment(int id)
        {
            if (id != 0)
            {
                var data = commentService.GetById(id);
                return View(data);
            }
            return Redirect("~/comment");
        }

        [HttpPost]
        public IActionResult UpdateComment(Comment comment)
        {
            if (comment.CommentID != 0)
            {
                commentService.UpdateComment(comment);
            }
            return Redirect("~/comment");
        }

        [Route("delete-comment")]
        public IActionResult DeleteComment(int id)
        {
            if (id != 0)
            {
                commentService.DeleteComment(id);
            }
            return Redirect("~/comment");
        }
    }
}
