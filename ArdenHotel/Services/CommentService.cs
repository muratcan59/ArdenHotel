using ArdenHotel.Data;
using ArdenHotel.Entities;
using ArdenHotel.Models;
using ArdenHotel.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArdenHotel.Services
{
    public class CommentService : ICommentService
    {
        private IGenericRepository<Comment> _repository;
        private ApplicationDbContext context;
        public CommentService(IGenericRepository<Comment> repository, ApplicationDbContext context)
        {
            _repository = repository;
            this.context = context;
        }

        public void DeleteComment(int id)
        {
            if (id != 0)
            {
                _repository.Delete(id);
            }

        }

        public Comment GetById(int id)
        {
            if (id != 0)
            {
                var commentID = _repository.Get(x => x.CommentID == id);
                return commentID;
            }
            return null;
        }

        public CommentViewModel GetList()
        {
            var allCommentList = _repository.GetList().Where(x => x.RoomID == null || x.RoomID == 0).ToList();
            CommentViewModel commentViewModel;
            if (allCommentList.Count > 0)
            {
                commentViewModel = new CommentViewModel { Comments = allCommentList };
            }
            else
            {
                commentViewModel = new CommentViewModel { Comments = new List<Comment>() };
            }
            return commentViewModel;
        }

        public List<CommentViewModel> GetListWithRoom()
        {
            var list = (from comment in context.Set<Comment>()
                        join room in context.Set<Room>() on comment.RoomID equals room.RoomID
                        into temp
                        from t in temp.DefaultIfEmpty()
                        select new CommentViewModel
                        {
                            Room = t,
                            Comment = comment
                        }).ToList();

            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return new List<CommentViewModel>();
            }
        }

        public Comment InsertComment(Comment comment)
        {
            if (comment != null)
            {
                _repository.Add(comment);
            }
            return null;
        }

        public void UpdateComment(Comment comment)
        {
            if (comment != null)
            {
                _repository.Update(comment);
            }
        }
    }
}
