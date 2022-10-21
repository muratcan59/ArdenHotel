using ArdenHotel.Entities;
using ArdenHotel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArdenHotel.Services
{
    public interface ICommentService
    {
        Comment GetById(int id);
        CommentViewModel GetList();
        List<CommentViewModel> GetListWithRoom();
        Comment InsertComment(Comment comment);
        void DeleteComment(int id);
        void UpdateComment(Comment comment);
    }
}
