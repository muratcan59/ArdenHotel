using ArdenHotel.Entities;
using System.Collections.Generic;

namespace ArdenHotel.Models
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            Comments = new List<Comment>();
            Comment  = new Comment();
            Room = new Room();
        }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public Room Room { get; set; }
    }
}
