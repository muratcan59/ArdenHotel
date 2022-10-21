using ArdenHotel.Entities;
using ArdenHotel.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArdenHotel.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [Route("comments")]
        public IActionResult Index()
        {
            var data = commentService.GetList();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertComment(Comment comment)
        {
            if (comment != null)
            {
                if (ModelState.IsValid)
                {
                    comment.CreatedDateUtc = DateTime.UtcNow;
                    commentService.InsertComment(comment);
                }
                return (comment.RoomID != null && comment.RoomID != 0) ? Redirect("~/room-detail?id=" + comment.RoomID) : Redirect("~/comments");
            }
            return View();

        }
    }
}
